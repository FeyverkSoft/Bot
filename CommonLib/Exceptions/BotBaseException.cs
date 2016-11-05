using System;

namespace CommonLib.Exceptions
{
    [Serializable]
    public class BotBaseException : Exception
    {
        /// <summary>
        /// Код исключения
        /// </summary>
        public String Code { get; private set; } = String.Empty;

        public BotBaseException() : base() { }
        public BotBaseException(String code, String message = "", Exception ex = null) : base(message, ex)
        {
            if (code != null)
            {
                Code = code;
            }
        }

    }
}
