using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Core.ActionExecutors.ExecutorResult;
using Core.ConfigEntity;
using Core.Events;

namespace Core.Core
{
    /// <summary>
    /// Ядро исполнителя действий
    /// </summary>
    public interface IExecutiveCore
    {
        /// <summary>
        /// Событие вывода сообщения
        /// </summary>
        event PrintMessageEvent OnPrintMessageEvent;

        event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Текущий статус ядра
        /// </summary>
        CoreStatus Status { get; }
        /// <summary>
        /// Прервать выполнение 
        /// </summary>
        void Abort();
        /// <summary>
        /// Запустить исполнителя идействий описанных в конфигурации
        /// </summary>
        Task<IExecutorResult> Run(Config config);
        /// <summary>
        /// Выполнить список действий
        /// </summary>
        /// <param name="actions">Список действий к исполнению</param>
        Task<IExecutorResult> Run(ListBotAction actions);
        /// <summary>
        /// Выполнить одно действие бота
        /// </summary>
        /// <param name="action">Действие к исполнению ботом</param>
        IExecutorResult Run(IBotAction action);

        /// <summary>
        /// Версия ядра исполнителя
        /// </summary>
        Version Version { get; }
    }
}