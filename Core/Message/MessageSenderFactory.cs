using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Message
{
    /// <summary>
    /// Фабрика отправителей сообщений
    /// </summary>
    internal static class MessageSenderFactory
    {
        private static readonly Dictionary<EMessageType, IMessageSender> MessageSenders = new Dictionary<EMessageType, IMessageSender>();
        /// <summary>
        /// Возвращает необходимый сендер для данного типа сообщения
        /// </summary>
        /// <param name="senderType"></param>
        /// <returns></returns>
        public static IMessageSender GetSender(EMessageType senderType)
        {
            if (!MessageSenders.ContainsKey(senderType))
                switch (senderType)
                {
                    case EMessageType.Email:
                        MessageSenders.Add(senderType, new EmailMessageSender());
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(senderType), senderType, null);
                }
            return MessageSenders[senderType];
        }
    }
}
