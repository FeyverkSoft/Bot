using System;
using System.Reflection;
using Core.ConfigEntity;
using Core.ConfigEntity.ActionObjects;
using Core.Events;
using Core.ActionExecutors;
using Core.Helpers;
using System.Threading.Tasks;
using LogWrapper;

namespace Core.Core
{
    /// <summary>
    /// Ядро исполнителя действий
    /// </summary>
    public sealed class DefaultExecutiveCore : IExecutiveCore
    {
        private Boolean IsAbort { get; set; } = false;

        /// <summary>
        /// Фабрика действий
        /// </summary>
        private readonly IActionFactory ActionFactory;
        /// <summary>
        /// Событие вывода сообщения
        /// </summary>
        public event PrintMessageEvent OnPrintMessageEvent;
        /// <summary>
        /// Вывести сообщение
        /// </summary>
        /// <param name="o"></param>
        private void Print(Object o)
        {
            Log.WriteLine(o);
            OnPrintMessageEvent?.Invoke(o?.ToJson(true, false));
        }

        public DefaultExecutiveCore(IActionFactory actionFactory = null)
        {
            if (actionFactory == null)
                ActionFactory = new DefaultActionFactory();
        }

        /// <summary>
        /// Запустить исполнителя идействий описанных в конфигурации
        /// </summary>
        public async void Run(Config config)
        {
            if (config == null)
                throw new NullReferenceException(nameof(config));

            if (config.BotVer != Assembly.GetExecutingAssembly().GetName().Version)
                Print(new { Status = EStatus.Warning, Message = $"Версия конфигурауционного файла не совпадает сверсией интерпретатора! Возможны побочные эффекты.", Date = DateTime.Now });

            await Task.Factory.StartNew(() => Run(config.Actions));
        }
        /// <summary>
        /// Выполнить список действий
        /// </summary>
        /// <param name="actions">Список действий к исполнению</param>
        public async void Run(ListBotAction actions)
        {
            if (actions == null)
                throw new NullReferenceException(nameof(actions));
            Print(new { Status = EStatus.Info, Message = $"--BEGIN--{Environment.NewLine}{nameof(DefaultExecutiveCore)}.Run()", Date = DateTime.Now });
            IsAbort = false;
            await Task.Factory.StartNew(() => InternalIterator(actions));
            Print(new { Status = EStatus.Info, Message = $"--END--{Environment.NewLine}{nameof(DefaultExecutiveCore)}.Run()", Date = DateTime.Now });
        }
        /// <summary>
        /// Выполнить одно действие бота
        /// </summary>
        /// <param name="action">Действие к исполнению ботом</param>
        public void Run(IBotAction action)
        {
            if (action == null)
                throw new NullReferenceException(nameof(action));
            InternalActRun(action);
        }

        /// <summary>
        /// Функция производит прирывание выполнения ядра бота
        /// </summary>
        public void Abort()
        {
            Print(new { Status = EStatus.Abort, Message = $"{nameof(DefaultExecutiveCore)}.Abort()", Date = DateTime.Now });
            IsAbort = true;
        }

        /// <summary>
        /// Внутреняя логика выполнения действий
        /// </summary>
        /// <param name="actions"></param>
        private void InternalIterator(ListBotAction actions)
        {
            //Если процес находится в процессе отмены, то прирываем итерации
            if (IsAbort)
                return;
            foreach (IBotAction act in actions)
            {
                InternalActRun(act);
            }
        }

        /// <summary>
        /// Выполнить одно действие бота
        /// </summary>
        /// <param name="action">Действие к исполнению ботом</param>
        private void InternalActRun(IBotAction action)
        {
            if (!action.IsValid)
            {
                Print(new
                {
                    Message = $"При выполнении действия {action.ActionType}, произошла ошибка. {Environment.NewLine}Тип действия не совпадает с членами действия.",
                    Date = DateTime.Now,
                    Status = EStatus.Abort
                });
                IsAbort = true;
            }
            if (IsAbort)
                return;

            switch (action.ActionType)//Логика для особых, не фабричных действий
            {
                case ActionType.Loop:
                    foreach (LoopAct subAct in action.SubActions)
                    {
                        for (var i = subAct.IterationCount; i > 0; i--)
                        {
                            Print(new { Message = $"Input loop; iterationCount:{subAct.IterationCount}", Status = EStatus.Info });
                            InternalIterator(subAct.Actions);// Рекурсия :)
                        }
                    }
                    break;
                default:
                    IExecutor executor = ActionFactory.GetExecutorAction(action.ActionType);
                    executor.OnPrintMessageEvent += OnPrintMessageEvent;//Подписываем и исполнителя на выхлоп
                    executor.Invoke(action.SubActions);
                    break;
            }
            if (IsAbort)
                return;
        }
    }
}
