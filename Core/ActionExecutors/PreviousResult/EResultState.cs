using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ActionExecutors.PreviousResult
{
    /// <summary>
    /// Тип результата выполнения действия
    /// Успешен или не успешен
    /// </summary>
    [Flags]
    public enum EResultState
    {
        /// <summary>
        /// Успех
        /// </summary>
        Success = 2,
        /// <summary>
        /// Провал
        /// </summary>
        Failure = 4,
        /// <summary>
        /// Ошибка, дальнейшее выполнение не рекомендуется
        /// </summary>
        Error = 8,
        /// <summary>
        /// Нет результата о выполнении или его не возможно получить
        /// </summary>
        NoResult = 16
    }
}
