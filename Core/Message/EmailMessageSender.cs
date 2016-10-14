using System;

namespace Core.Message
{
    /// <summary>
    /// Реализация отправки сообщения по средствам email сообщения
    /// </summary>
    internal class EmailMessageSender : IMessageSender
    {
        /// <summary>
        /// Отправить сообщение указанному адресату
        /// </summary>
        /// <param name="body">Тело сообщения</param>
        /// <param name="recipient">Получатель сообщения</param>
        /// <returns></returns>
        public MessageResult SendMessage(String body, String recipient)
        {
            throw new NotImplementedException();
        }
    }
}