using Executor.Events;

namespace Executor.Core
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
        /// Запустить выполнителя
        /// </summary>
        void Run();
    }
}