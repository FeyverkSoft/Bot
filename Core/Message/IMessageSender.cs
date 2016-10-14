using System;

namespace Core.Message
{
    /// <summary>
    /// Интерфейс отправителя сообщений
    /// </summary>
    internal interface IMessageSender
    {
        /// <summary>
        /// Отправить сообщение указанному адресату
        /// </summary>
        /// <param name="body">Тело сообщения</param>
        /// <param name="recipient">Получатель сообщения</param>
        /// <returns></returns>
        MessageResult SendMessage(String body, String recipient);
    }
}
