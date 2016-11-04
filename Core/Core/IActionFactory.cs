using Core.ActionExecutors;
using Core.ConfigEntity;

namespace Core.Core
{
    /// <summary>
    /// Интерфейс фабрики действий
    /// </summary>
    public interface IActionExecutorFactory
    {
        /// <summary>
        /// Возвращает исполнителя по типу действия
        /// </summary>
        /// <param name="type">Тип действия для которого происходит поиск фабрики</param>
        /// <returns></returns>
        IExecutor GetExecutorAction(ActionType type);
    }
}