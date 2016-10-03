using Core.Core;

namespace Core.ActionExecutors.ExecutorResult
{
    /// <summary>
    /// Результат выполнения вункции поиска и ожидания окна
    /// </summary>
    public class ExpectWindowExecutorResult : BaseExecutorResult
    {
        /// <summary>
        /// Результат выполнения
        /// </summary>
        public WinInfo ExecutorResult { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="executorResult">Информация о найденном окне</param>
        /// <param name="executorState"></param>
        public ExpectWindowExecutorResult(WinInfo executorResult, EResultState executorState = EResultState.Success) : base(executorState)
        {
            ExecutorResult = executorResult;
        }
    }
}
