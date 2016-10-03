using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ActionExecutors.PreviousResult
{
    /// <summary>
    /// Базовый метод для возвращения результата
    /// </summary>
    public class BaseExecutorResult : IPreviousResult
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
