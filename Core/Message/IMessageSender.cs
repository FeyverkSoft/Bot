using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// <returns></returns>
        MessageResult SendMessage();
    }
}
