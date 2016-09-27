using System;
using System.Reflection;
using Executor.ConfigEntity;
using Executor.ConfigEntity.ActionObjects;
using Executor.Events;
using Executor.ActionExecutors;
using Executor.Helpers;
using System.Threading.Tasks;
using System.Threading;

namespace Executor.Core
{
    /// <summary>
    /// Ядро исполнителя действий
    /// </summary>
    public sealed class DefaultExecutiveCore : IExecutiveCore
    {
        private Boolean IsAbort { get; set; } = false;
        /// <summary>
        /// Конфигурация бота
        /// </summary>
        private readonly Config Config;

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
            OnPrintMessageEvent?.Invoke(o?.ToJson(true, false));
        }

        public DefaultExecutiveCore(Config config, IActionFactory actionFactory = null)
        {
            if (config == null)
                throw new NullReferenceException(nameof(config));
            Config = config;
            if (actionFactory == null)
                ActionFactory = new DefaultActionFactory();
        }

        /// <summary>
        /// Функция производит запуск ядра бота
        /// </summary>
        public async void Run()
        {
            Print(new { Status = EStatus.Info, Message = $"--BEGIN--{Environment.NewLine}{nameof(DefaultExecutiveCore)}.Run()", Date = DateTime.Now });
            IsAbort = false;
            if (Config.BotVer != Assembly.GetExecutingAssembly().GetName().Version)
                Print(new { Status = EStatus.Warning, Message = $"Версия конфигурауционного файла не совпадает сверсией интерпретатора! Возможны побочные эффекты.", Date = DateTime.Now });

            await Task.Factory.StartNew(() => InternalIterator(Config.Actions));

            Print(new { Status = EStatus.Info, Message = $"--END--{Environment.NewLine}{nameof(DefaultExecutiveCore)}.Run()", Date = DateTime.Now });
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
                if (!act.IsValid)
                {
                    Print(new
                    {
                        Message = $"При выполнении действия {act.ActionType}, произошла ошибка. {Environment.NewLine}Тип действия не совпадает с членами действия.",
                        Date = DateTime.Now,
                        Status = EStatus.Abort
                    });
                    IsAbort = true;
                }
                if (IsAbort)
                    return;

                switch (act.ActionType)//Логика для особых, не фабричных действий
                {
                    case ActionType.Loop:
                        foreach (LoopAct subAct in act.SubActions)
                        {
                            for (var i = subAct.IterationCount; i > 0; i--)
                            {
                                Print(new { Message = $"Input loop; iterationCount:{subAct.IterationCount}", Status = EStatus.Info });
                                InternalIterator(subAct.Actions);// Рекурсия :)
                            }
                        }
                        break;
                    default:
                        IExecutor executor = ActionFactory.GetExecutorAction(act.ActionType);
                        executor.OnPrintMessageEvent += OnPrintMessageEvent;//Подписываем и исполнителя на выхлоп
                        executor.Invoke(act.SubActions);
                        break;
                }
                if (IsAbort)
                    return;
            }
        }
    }
}
