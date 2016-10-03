using System;

namespace Core.ActionExecutors.ExecutorResult
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
        Success = 0x0,
        /// <summary>
        /// Провал
        /// </summary>
        Failure = 0x1,
        /// <summary>
        /// Ошибка, дальнейшее выполнение не рекомендуется
        /// </summary>
        Error = 0x2,
        /// <summary>
        /// Нет результата о выполнении или его не возможно получить
        /// </summary>
        NoResult = 0x4
    }
}
