using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Core;

namespace Core.ActionExecutors.ExecutorResult
{
    public class ObjectExecutorResult : BaseExecutorResult
    {
        /// <summary>
        /// Результат выполнения
        /// </summary>
        public ObjectInfo ExecutorResult { get; }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="object">Объект</param>
        /// <param name="executorState">Статус результата исполнения</param>
        public ObjectExecutorResult(ObjectInfo @object ,EResultState executorState = EResultState.Success) : base(executorState)
        {
            ExecutorResult = @object;
        }
    }
}
