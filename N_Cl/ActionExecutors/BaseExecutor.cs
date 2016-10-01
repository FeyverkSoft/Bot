using System;
using Core.ConfigEntity.ActionObjects;
using Core.Events;
using Core.Helpers;

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
            OnPrintMessageEvent?.Invoke(o?.ToJson(formatting, false));
        }

        /// <summary>
        /// Вызвать выполнение действия у указанной фfбрики
        /// </summary>
        /// <param name="action">Список действи которые должен выполнить исполнитель</param>
        /// <returns></returns>
        public abstract Boolean Invoke(ListAction actions);
    }
}
