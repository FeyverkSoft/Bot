namespace Core.ActionExecutors.ExecutorResult
{
    /// <summary>
    /// Базовый метод для возвращения результата
    /// </summary>
    public class BaseExecutorResult : IExecutorResult
    {
        /// <summary>
        /// Результат выполнения действия, успех/не успех, ошибка
        /// </summary>
        public EResultState State { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="executorState">Статус результата исполнения</param>
        public BaseExecutorResult(EResultState executorState = (EResultState.Success & EResultState.NoResult))
        {
            State = executorState;
        }
    }
}
