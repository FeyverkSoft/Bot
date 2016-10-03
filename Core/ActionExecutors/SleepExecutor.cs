using System;
using System.Globalization;
using System.Threading;
using Core.ActionExecutors.ExecutorResult;
using Core.ConfigEntity.ActionObjects;
using Core.Core;

namespace Core.ActionExecutors
{
    /// <summary>
    /// Исполнитель действия сна потока=
    /// </summary>
    internal sealed class SleepExecutor : BaseExecutor
    {
        Random rand = new Random();
        /// <summary>
        /// Вызвать выполнение действия у указанной фабрики
        /// </summary>
        /// <param name="actions">Список действи которые должен выполнить исполнитель</param>
        /// <param name="previousResult">Результат выполнения предыдущего действия, (не обязательно :))</param>
        /// <returns></returns>
        public override IExecutorResult Invoke(ListAct actions, IExecutorResult previousResult = null)
        {
            Print(new { Date = DateTime.Now.ToString(CultureInfo.InvariantCulture), Message = $"{GetType().Name}.{nameof(Invoke)}(actions.Count:{actions?.Count ?? -1})", Status = EStatus.Info }, false);
            try
            {
                //:D как-то тупо, несколько действий сна подряд....
                if (actions != null)
                    foreach (SleepAct action in actions)
                    {
                        Thread.Sleep(action.Delay + rand.Next(0, action.MaxRandDelay));
                    }
            }
            catch (Exception ex)
            {
                Print(new { Date = DateTime.Now.ToString(CultureInfo.InvariantCulture), ex });
                return new BaseExecutorResult(EResultState.Error & EResultState.NoResult);
            }
            return previousResult ?? new BaseExecutorResult();
        }
    }
}