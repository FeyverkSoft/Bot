using CommonLib.Attributes;

namespace Core.Message
{
    /// <summary>
    /// Содержит все возможные типы сообщений, такие как E-Mail
    /// </summary>
    [LocDescription("EMessageType", typeof(Resources.CoreText))]
    public enum EMessageType
    {
        /// <summary>
        /// Отправить сообщение на емайл
        /// </summary>
        [LocDescription("EMessageType_Email", typeof(Resources.CoreText))]
        Email
    }
}
