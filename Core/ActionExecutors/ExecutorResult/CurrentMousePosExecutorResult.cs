using Core.Core;

namespace Core.ActionExecutors.ExecutorResult
{
    /// <summary>
    /// Позация курсора мышки на момент вызова метода получения позиции
    /// </summary>
    public class CurrentMousePosExecutorResult:BaseExecutorResult
    {
        /// <summary>
        /// Результат выполнения
        /// </summary>
        public Point ExecutorResult { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="executorResult"></param>
        /// <param name="executorState">Статус выполнения</param>
        public CurrentMousePosExecutorResult(Point executorResult, EResultState executorState = EResultState.Success) : base(executorState)
        {
            ExecutorResult = executorResult;
        }
    }
}
