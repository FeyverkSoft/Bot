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
    public class BasePreviousResult : IPreviousResult
    {
        /// <summary>
        /// Результат выполнения действия, успех/не успех, ошибка
        /// </summary>
        public EExecutorResultState State { get; private set } = EExecutorResultState.NoResult & EExecutorResultState.Success;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="executorResult">Статус результата исполнения</param>
        public BasePreviousResult(EExecutorResultState executorResult)
        {
            State = executorResult;
        }
    }
}
