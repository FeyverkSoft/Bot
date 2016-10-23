using System.ComponentModel;
using Core.Attributes;

namespace Core.Message
{
    /// <summary>
    /// Содержит все возможные типы сообщений, такие как E-Mail
    /// </summary>
    [LocDescription("EMessageType")]
    public enum EMessageType
    {
        /// <summary>
        /// Отправить сообщение на емайл
        /// </summary>
        [LocDescription("EMessageType_Email")]
        Email
    }
}
