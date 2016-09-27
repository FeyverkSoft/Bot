using Executor.ActionExecutors;
using Executor.ConfigEntity;

namespace Executor.Core
{
    /// <summary>
    /// Интерфейс фабрики действий
    /// </summary>
    public interface IActionFactory
    {
        /// <summary>
        /// Возвращает исполнителя по типу действия
        /// </summary>
        /// <param name="type">Тип действия для которого происходит поиск фабрики</param>
        /// <returns></returns>
        IExecutor GetExecutorAction(ActionType type);
    }
}