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
