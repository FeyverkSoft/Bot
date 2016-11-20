using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Core.ConfigEntity;
using Core.ConfigEntity.ActionObjects;
using Core.Events;
using Core.Helpers;
using System.Threading.Tasks;
using CommonLib.Exceptions;
using Core.ActionExecutors.ExecutorResult;
using Core.Resources;
using LogWrapper;

namespace Core.Core
{
    /// <summary>
    /// Ядро исполнителя действий
    /// </summary>
    internal sealed class DefaultExecutiveCore : IExecutiveCore
    {
        private Boolean IsAbort
        {
            get { return _isAbort; }
            set { _isAbort = value; }
        }

        /// <summary>
        /// Текущий статус ядра
        /// </summary>
        public CoreStatus Status
        {
            get { return _status; }
            private set
            {
                _status = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Версия ядра исполнителя
        /// </summary>
        public Version Version => Assembly.GetExecutingAssembly().GetName().Version;

        /// <summary>
        /// Фабрика действий
        /// </summary>
        private readonly IActionExecutorFactory _actionFactory;

        /// <summary>
        /// Валидатор команд
        /// </summary>
        public IConfigValidator ConfigValidator { get; }

        private CoreStatus _status = CoreStatus.Stop;
        private Boolean _isAbort = false;

        private readonly Stack<IExecutorResult> _executorResultStack = new Stack<IExecutorResult>();

        /// <summary>
        /// Событие вывода сообщения
        /// </summary>
        public event PrintMessageEvent OnPrintMessageEvent;

        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        internal void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        /// <summary>
        /// Вывести сообщение
        /// </summary>
        /// <param name="o"></param>
        private void Print(Object o)
        {
            Log.WriteLine(o);
            OnPrintMessageEvent?.Invoke(o?.ToJson(true, false));
        }

        public DefaultExecutiveCore(IActionExecutorFactory actionFactory, IConfigValidator validator)
        {
            _actionFactory = actionFactory;
            ConfigValidator = validator;
            Assemblys.LoadPlugins();
        }

        /// <summary>
        /// Запустить исполнителя идействий описанных в конфигурации
        /// </summary>
        public async Task<IExecutorResult> Run(Config config)
        {
            if (config == null)
                throw new NullReferenceException(nameof(config));

            if (config.BotVer != Version)
                Print(new
                {
                    Status = EStatus.Warning,
                    Message = CoreText.DifferentConfigVersions,
                    Ifo = $"{config.BotVer} != {Version}",
                    Date = DateTime.Now
                });

            return await Run(config.Actions);
        }
        /// <summary>
        /// Выполнить список действий
        /// </summary>
        /// <param name="actions">Список действий к исполнению</param>
        public async Task<IExecutorResult> Run(ListBotAction actions)
        {
            if (actions == null)
                throw new NullReferenceException(nameof(actions));
            Print(new { Status = EStatus.Info, Message = $"--BEGIN--{Environment.NewLine}{nameof(DefaultExecutiveCore)}.Run()", Date = DateTime.Now });
            IsAbort = false;
            var res = await Task.Factory.StartNew(() => InternalIterator(actions, new BaseExecutorResult()));
            Status = CoreStatus.Stop;
            Print(new { Status = EStatus.Info, Message = $"--END--{Environment.NewLine}{nameof(DefaultExecutiveCore)}.Run()", Date = DateTime.Now });
            return res;
        }
        /// <summary>
        /// Выполнить одно действие бота
        /// </summary>
        /// <param name="action">Действие к исполнению ботом</param>
        public IExecutorResult Run(IBotAction action)
        {
            _executorResultStack.Clear();
            if (action == null)
                throw new NullReferenceException(nameof(action));
            return CoreV2(new ListBotAction { action }, null);
        }


        /// <summary>
        /// Функция производит прирывание выполнения ядра бота
        /// </summary>
        public void Abort()
        {
            Abort(null);
        }

        private void Abort(IBotAction act)
        {
            _executorResultStack.Clear();
            Print(new
            {
                Status = EStatus.Abort,
                Message = $"{nameof(DefaultExecutiveCore)}.Abort()",
                Date = DateTime.Now,
                Reason = act != null ? String.Format(CoreText.IncorrectAction, act.ActionType, String.Empty) : String.Empty
            });
            IsAbort = true;
            Status = CoreStatus.Stop;
        }

        ///  <summary>
        ///  Внутреняя логика выполнения действий
        ///  </summary>
        ///  <param name="actions"></param>
        /// <param name="res"></param>
        /// <return>Возвращает результат последнего действия</return>
        private IExecutorResult InternalIterator(ListBotAction actions, IExecutorResult res)
        {
            _executorResultStack.Clear();
            Status = CoreStatus.Run;
            IsAbort = false;
            var errorList = ConfigValidator.GetErrorList(actions);
            if (errorList.Count > 0)
            {
                Print(errorList);
                Abort();
                return null;
            }
            foreach (var action in actions)
            {
                action.IsCurrent = false;
            }
            return CoreV2(actions, res);
        }

        private IExecutorResult CoreV2(ListBotAction actions, IExecutorResult res)
        {
            try
            {
                for (var i = 0; i < actions.Count; i++)
                {
                    if (IsAbort)
                        return null;
                    var currentAction = actions[i];
                    currentAction.IsCurrent = true;
                    if (!currentAction.IsValid)
                        Abort(currentAction);
                    var executor = _actionFactory.GetExecutorAction(currentAction.ActionType);
                    if (executor != null)//не для всех действий они могут и быть, например стек GOTO Loop
                        executor.OnPrintMessageEvent += OnPrintMessageEvent; //Подписываем и исполнителя на выхлоп

                    switch (currentAction.ActionType) //Логика для особых, не фабричных действий
                    {
                        case ActionType.Stack:
                            {
                                var action = currentAction.SubActions.Cast<StackAct>().FirstOrDefault()?.Action;
                                switch (action)
                                {
                                    case EStackAction.Pop:
                                        res = _executorResultStack.Pop();
                                        break;
                                    case EStackAction.Push:
                                        _executorResultStack.Push(res);
                                        break;
                                    case EStackAction.Peek:
                                        res = _executorResultStack.Peek();
                                        break;
                                    case EStackAction.Clear:
                                        _executorResultStack.Clear();
                                        break;
                                }
                            }
                            break;
                        case ActionType.Label:
                            break;
                        case ActionType.GOTO:
                            {
                                var label = currentAction.SubActions.Cast<GoToAct>().FirstOrDefault()?.LabelName;
                                i = FindLabel(actions, label);
                            }
                            break;
                        case ActionType.Loop:
                            {
                                foreach (var subAct in currentAction.SubActions.Cast<LoopAct>())
                                {
                                    for (var j = subAct.IterationCount; j > 0; j--)
                                    {
                                        res = CoreV2(subAct.Actions, res); // Рекурсия :)
                                    }
                                }
                            }
                            break;
                        case ActionType.If: // пока что нет идей что и как можно проверять
                            {
                                if (currentAction.SubActions.Count > 0)
                                {
                                    //Подписываем и исполнителя на выхлоп
                                    var ifRes = executor.Invoke(currentAction, ref _isAbort, res);
                                    if (ifRes.State == EResultState.Success && ifRes is BooleanExecutorResult)
                                    {
                                        var subAct = currentAction.SubActions.Cast<IfAction>().First();

                                        var label = ((BooleanExecutorResult)ifRes).ExecutorResult
                                            ? subAct.SuccessLabel
                                            : subAct.FailLabel;
                                        // поиск метки с таким название в нутри области видимости
                                        i = FindLabel(actions, label);
                                    }
                                }
                                break;
                            }
                        default:
                            res = currentAction.SubActions != null && currentAction.SubActions.Count > 0
                                ? executor.Invoke(currentAction, ref _isAbort, res)
                                : executor.Invoke(ref _isAbort, res);
                            break;
                    }
                    if (executor != null)
                        executor.OnPrintMessageEvent -= OnPrintMessageEvent;
                    currentAction.IsCurrent = false;
                }
            }
            catch (Exception ex)
            {
                Status = CoreStatus.Stop;
                Log.WriteLine(ex, LogLevel.Error);
                Print(ex);
                throw;
            }
            return res;
        }

        private Int32 FindLabel(ListBotAction actions, String label)
        {
            for (var k = 0; k < actions.Count; k++)
            {
                if (actions[k].ActionType == ActionType.Label)
                {
                    if (label.Equals(((LabelAct)actions[k].SubActions.FirstOrDefault())?.LabelName,
                        StringComparison.InvariantCultureIgnoreCase))
                    {
                        return k;
                    }
                }
            }
            throw new BotBaseException("Label not found!");
        }
    }
}
