using System;

namespace Core.Message
{
    /// <summary>
    /// Результат отправки сообщения
    /// </summary>
    public class MessageResult
    {
        /// <summary>
        /// Статус отправки
        /// </summary>
        public EMessageStatus Status { get; set; }
        /// <summary>
        /// Причина отказа или ошибки, если статус не успешен
        /// </summary>
        public String Reason { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status">Статус отправки</param>
        /// <param name="reason"> Причина отказа или ошибки, если статус не успешен</param>
        public MessageResult(EMessageStatus status = EMessageStatus.Success, String reason = "" )
        {
            Status = status;
            Reason = reason;
        }
        
    }
}