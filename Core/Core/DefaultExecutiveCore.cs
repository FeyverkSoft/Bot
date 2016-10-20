using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Core.ConfigEntity;
using Core.ConfigEntity.ActionObjects;
using Core.Events;
using Core.Helpers;
using System.Threading.Tasks;
using Core.ActionExecutors.ExecutorResult;
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
        private readonly IActionFactory _actionFactory;

        private CoreStatus _status = CoreStatus.Stop;
        private bool _isAbort = false;

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

        public DefaultExecutiveCore(IActionFactory actionFactory)
        {
            _actionFactory = actionFactory;
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
            if (action == null)
                throw new NullReferenceException(nameof(action));
            return InternalActRun(action, null);
        }


        /// <summary>
        /// Функция производит прирывание выполнения ядра бота
        /// </summary>
        public void Abort()
        {
            Print(new { Status = EStatus.Abort, Message = $"{nameof(DefaultExecutiveCore)}.Abort()", Date = DateTime.Now });
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
            Status = CoreStatus.Run;
            IsAbort = false;
            //Если процес находится в процессе отмены, то прирываем итерации
            if (IsAbort || res.State == EResultState.Error)
            {
                Status = CoreStatus.Stop;
                return null;
            }

            return actions.Aggregate(res, (current, act) => InternalActRun(act, current));
        }

        ///  <summary>
        ///  Выполнить одно действие бота
        ///  </summary>
        ///  <param name="action">Действие к исполнению ботом</param>
        /// <param name="res"></param>
        /// <return>Возвращает результат действия</return>
        private IExecutorResult InternalActRun(IBotAction action, IExecutorResult res)
        {
            try
            {
                if (!action.IsValid)
                {
                    Print(new
                    {
                        Message = String.Format(CoreText.IncorrectAction, action.ActionType, Environment.NewLine),
                        Date = DateTime.Now,
                        Status = EStatus.Abort
                    });
                    IsAbort = true;
                }
                if (IsAbort)
                    return null;

                var executor = _actionFactory.GetExecutorAction(action.ActionType);
                switch (action.ActionType)//Логика для особых, не фабричных действий
                {
                    case ActionType.Loop:
                        {
                            foreach (var subAct in action.SubActions.Cast<LoopAct>())
                            {
                                if (IsAbort)
                                    return null;
                                for (var i = subAct.IterationCount; i > 0; i--)
                                {
                                    if (IsAbort || res?.State == EResultState.Error)
                                        return null;
                                    Print(
                                        new
                                        {
                                            Message = $"Input loop; iterationCount:{subAct.IterationCount}",
                                            Status = EStatus.Info
                                        });
                                    res = InternalIterator(subAct.Actions, res); // Рекурсия :)
                                }
                            }
                            return res;
                        }
                    case ActionType.If: // пока что нет идей что и как можно проверять
                        {
                            if (action.SubActions.Count > 0)
                            {
                                executor.OnPrintMessageEvent += OnPrintMessageEvent; //Подписываем и исполнителя на выхлоп
                                var ifRes = executor.Invoke(action.SubActions, ref _isAbort, res);
                                if (ifRes.State == EResultState.Success && ifRes is BooleanExecutorResult)
                                {
                                    return InternalIterator(((BooleanExecutorResult)ifRes).ExecutorResult
                                        ? (action as IfAction).Actions
                                        : (action as IfAction).FailActions, res);
                                }
                                IsAbort = true;
                                throw new Exception("Incorrect If>BooleanExecutorResult!");
                            }
                            return res;
                        }
                    default:
                        {
                            executor.OnPrintMessageEvent += OnPrintMessageEvent; //Подписываем и исполнителя на выхлоп
                            return action.SubActions != null && action.SubActions.Count > 0
                                ? executor.Invoke(action.SubActions, ref _isAbort, res)
                                : executor.Invoke(ref _isAbort, res);
                        }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLine(ex, LogLevel.Error);
                throw;
            }
        }
    }
}
