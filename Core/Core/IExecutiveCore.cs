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
        /// <summary>
        /// Прервать выполнение 
        /// </summary>
        void Abort();
        /// <summary>
        /// Запустить исполнителя идействий описанных в конфигурации
        /// </summary>
        void Run(Config config);
        /// <summary>
        /// Выполнить список действий
        /// </summary>
        /// <param name="actions">Список действий к исполнению</param>
        void Run(ListBotAction actions);
        /// <summary>
        /// Выполнить одно действие бота
        /// </summary>
        /// <param name="action">Действие к исполнению ботом</param>
        void Run(IBotAction action);
    }
}