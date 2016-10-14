namespace Core.Message
{
    /// <summary>
    /// Статус отправки сообщения
    /// </summary>
    public enum EMessageStatus
    {
        /// <summary>
        /// Сообщение доставленно успешно
        /// </summary>
        Success,
        /// <summary>
        /// При отправки сообщения возникли ошибки
        /// </summary>
        Fail,
        /// <summary>
        /// Отправка сообщения была отклонена
        /// </summary>
        Rejected
    }
}