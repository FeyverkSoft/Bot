﻿using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using Core.ConfigEntity;
using Core.ConfigEntity.ActionObjects;
using Core.Events;
using Core.ActionExecutors;
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
        private Boolean IsAbort { get; set; } = false;

        /// <summary>
        /// Фабрика действий
        /// </summary>
        private readonly IActionFactory _actionFactory;
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

            if (config.BotVer != Assembly.GetExecutingAssembly().GetName().Version)
                Print(new
                {
                    Status = EStatus.Warning,
                    Message = $"Версия исполняемой конфигурации не совпадает сверсией интерпретатора! Возможны побочные эффекты.",
                    Ifo = $"{config.BotVer} != {Assembly.GetExecutingAssembly().GetName().Version}",
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
        }

        ///  <summary>
        ///  Внутреняя логика выполнения действий
        ///  </summary>
        ///  <param name="actions"></param>
        /// <param name="res"></param>
        /// <return>Возвращает результат последнего действия</return>
        private IExecutorResult InternalIterator(ListBotAction actions, IExecutorResult res)
        {
            //Если процес находится в процессе отмены, то прирываем итерации
            if (IsAbort || res.State == EResultState.Error)
                return null;

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
                        Message = $"При выполнении действия {action.ActionType}, произошла ошибка. {Environment.NewLine}Тип действия не совпадает с членами действия.",
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
                                var ifRes = executor.Invoke(action.SubActions, res);
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
                                ? executor.Invoke(action.SubActions, res)
                                : executor.Invoke(res);
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
