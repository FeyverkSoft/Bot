using System;
using Core.ActionExecutors.PreviousResult;
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
        internal void Print(Object o, Boolean formatting = true)
        {
            Log.WriteLine(o);
            OnPrintMessageEvent?.Invoke(o?.ToJson(formatting, false));
        }

        /// <summary>
        /// Вызвать выполнение действия у указанной фfбрики
        /// </summary>
        /// <param name="action">Список действи которые должен выполнить исполнитель</param>
        /// <param name="previousResult">Результат выполнения предыдущего действия, не обязательно</param>
        /// <returns></returns>
        public abstract IPreviousResult Invoke(ListAction actions, IPreviousResult previousResult = null);
    }
}
