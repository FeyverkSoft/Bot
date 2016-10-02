﻿using System;
using System.Threading;
using Core.ActionExecutors.PreviousResult;
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
        /// <param name="action">Список действи которые должен выполнить исполнитель</param>
        /// <param name="previousResult">Результат выполнения предыдущего действия, (не обязательно :))</param>
        /// <returns></returns>
        public override IBasePreviousResult Invoke(ListAction actions, IBasePreviousResult previousResult = null)
        {
            Print(new { Date = DateTime.Now.ToString(), Message = $"{GetType().Name}.{nameof(Invoke)}(actions.Count:{actions?.Count ?? -1})", Status = EStatus.Info }, false);
            try
            {
                //:D как-то тупо, несколько действий сна подряд....
                foreach (SleepAct action in actions)
                {
                    Thread.Sleep(action.Delay + rand.Next(0, action.MaxRandDelay));
                }
            }
            catch (Exception ex)
            {
                Print(new { Date = DateTime.Now.ToString(), ex });
                return false;
            }
            return true;
        }
    }
}