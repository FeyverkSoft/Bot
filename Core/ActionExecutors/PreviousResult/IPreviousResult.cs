using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ActionExecutors.PreviousResult
{
    /// <summary>
    ///Базовый интерфейс для хранения результата вернувшего предыдущем исполнителем действия
    /// </summary>
    public interface IPreviousResult
    {
        /// <summary>
        /// Результат выполнения действия, успех/не успех, ошибка
        /// </summary>
        EResultState State { get; }
    }
}
