using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using Core.ActionExecutors.ExecutorResult;
using Core.ConfigEntity;
using Core.ConfigEntity.ActionObjects;
using Core.Core;
using Core.Helpers;

namespace Core.ActionExecutors
{
    /// <summary>
    /// Исполнитель действия сна потока=
    /// </summary>
    internal sealed class SleepExecutor : BaseExecutor
    {
        readonly Random _rand = new Random();

        /// <summary>
        /// Вызвать выполнение действия у указанной фабрики
        /// </summary>
        /// <param name="actions">Список действи которые должен выполнить исполнитель</param>
        /// <param name="isAbort"></param>
        /// <param name="previousResult">Результат выполнения предыдущего действия, (не обязательно :))</param>
        /// <returns></returns>
        public override IExecutorResult Invoke(IActionsContainer actions, ref bool isAbort, IExecutorResult previousResult = null)
        {
            Print(new
            {
                Date = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                Message = new
                {
                    func = $"{GetType().Name}.{nameof(Invoke)}",
                    param = $"{nameof(actions)}: {actions.ToJson(false, false)} ;{nameof(previousResult)}: {previousResult?.ToJson(false, false)})"
                },
                Status = EStatus.Info
            }, false);
            try
            {
                //:D как-то тупо, несколько действий сна подряд....
                if (actions != null)
                    foreach (var action in actions.SubActions.Cast<SleepAct>())
                    {
                        if (isAbort)
                            return new BaseExecutorResult();
                        Thread.Sleep(action.Delay + _rand.Next(0, action.MaxRandDelay));
                    }
            }
            catch (Exception ex)
            {
                Print(new { Date = DateTime.Now.ToString(CultureInfo.InvariantCulture), ex });
                return new BaseExecutorResult(EResultState.Error & EResultState.NoResult);
            }
            return previousResult ?? new BaseExecutorResult();
        }

        /// <summary>
        /// Вызвать выполнение действия у указанной фабрики
        /// </summary>
        /// <param name="isAbort"></param>
        /// <param name="previousResult">Результат выполнения предыдущего действия, (не обязательно :))</param>
        /// <returns></returns>
        public override IExecutorResult Invoke(ref bool isAbort, IExecutorResult previousResult = null)
        {
            throw new NotSupportedException();
        }
    }
}