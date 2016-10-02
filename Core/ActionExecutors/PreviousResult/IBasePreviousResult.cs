using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ActionExecutors.PreviousResult
{
    /// <summary>
    ///Базовый класс для хранения результата вернувшего предыдущем исполнителем действия
    /// </summary>
    public interface IBasePreviousResult
    {
        /// <summary>
        /// Результат выполнения действия, успех/не успех, ошибка
        /// </summary>
        EExecutorResult ExecutorResult {get; }
    }
}
