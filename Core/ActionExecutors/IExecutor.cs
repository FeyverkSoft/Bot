using System;
using Core.ConfigEntity.ActionObjects;
using Core.Events;

namespace Core.ActionExecutors
{
    /// <summary>
    /// Базовый интерфейс исполнителя действий
    /// </summary>
    public interface IExecutor
    {
        /// <summary>
        /// Событие вывода сообщения
        /// </summary>
        event PrintMessageEvent OnPrintMessageEvent;

        /// <summary>
        /// Вызвать выполнение действия у указанной фfбрики
        /// </summary>
        /// <param name="action">Список действи которые должен выполнить исполнитель</param>
        /// <returns></returns>
        Boolean Invoke(ListAction action);
    }
}