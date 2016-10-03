using System;
using Core.ActionExecutors.ExecutorResult;
using Core.ConfigEntity.ActionObjects;
using Core.Events;
using Core.Helpers;
using LogWrapper;

namespace Core.ActionExecutors
{
    public abstract class BaseExecutor : IExecutor
    {
        /// <summary>
        /// Событие вывода сообщения
        /// </summary>
        public event PrintMessageEvent OnPrintMessageEvent;

        /// <summary>
        /// Вывести сообщение
        /// </summary>
        /// <param name="o"></param>
        /// <param name="formatting"></param>
        internal void Print(Object o, Boolean formatting = true)
        {
            Log.WriteLine(o);
            OnPrintMessageEvent?.Invoke(o?.ToJson(formatting, false));
        }

        /// <summary>
        /// Вызвать выполнение действия у указанной фfбрики
        /// </summary>
        /// <param name="actions">Список действи которые должен выполнить исполнитель</param>
        /// <param name="previousResult">Результат выполнения предыдущего действия, не обязательно</param>
        /// <returns></returns>
        public abstract IExecutorResult Invoke(ListAct actions, IExecutorResult previousResult = null);
    }
}
