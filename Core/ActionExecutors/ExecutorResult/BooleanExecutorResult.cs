using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ActionExecutors.ExecutorResult
{
    /// <summary>
    /// Результат выполнения функции которая возвращает булево значение
    /// </summary>
    public class BooleanExecutorResult : BaseExecutorResult
    {
        /// <summary>
        /// Результат выполнения
        /// </summary>
        public Boolean ExecutorResult { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="result">Результат исполнения функции</param>
        /// <param name="executorState">Статус выполнения</param>
        public BooleanExecutorResult(Boolean result, EResultState executorState = EResultState.Success) : base(executorState)
        {
            ExecutorResult = result;
        }
    }
}
