namespace Core.ActionExecutors.ExecutorResult
{
    /// <summary>
    ///Базовый интерфейс для хранения результата вернувшего предыдущем исполнителем действия
    /// </summary>
    public interface IExecutorResult
    {
        /// <summary>
        /// Результат выполнения действия, успех/не успех, ошибка
        /// </summary>
        EResultState State { get; }
    }
}
