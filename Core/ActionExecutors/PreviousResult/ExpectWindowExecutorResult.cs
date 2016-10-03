using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Core;

namespace Core.ActionExecutors.PreviousResult
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

        public ExpectWindowExecutorResult(WinInfo executorResult, EResultState executorState = EResultState.Success) : base(executorState)
        {
            ExecutorResult = executorResult;
        }
    }
}
