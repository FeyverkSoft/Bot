using System;
using Core.ActionExecutors.ExecutorResult;
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
        /// <param name="actions">Список действи которые должен выполнить исполнитель</param>
        /// <param name="previousResult">Результат выполнения предыдущего действия, (не обязательно :))</param>
        /// <returns></returns>
        IExecutorResult Invoke(ListAct actions, IExecutorResult previousResult = null);
    }
}