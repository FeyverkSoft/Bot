using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using Core.Helpers;
using Core.Message;

namespace Core.ConfigEntity.ActionObjects
{
    /// <summary>
    /// Действие отправки казанного сообщения на указанные параметры
    /// </summary>
    [DataContract]
    [Description("Действие отправки казанного сообщения на указанные параметры")]
    public class SendMessageAct : IAction
    {
        /// <summary>
        /// Тип сообщения, например сообщение на емайл
        /// </summary>
        [DataMember]
        [Description("Тип сообщения, например сообщение на емайл")]
        public EMessageType MessageType { get; }
        /// <summary>
        /// Тело сообщения
        /// </summary>
        [DataMember]
        [Description("Тело сообщения, которое необходимо отправить")]
        public String Body { get; }
        /// <summary>
        /// Получатель сообщения
        /// </summary>
        [DataMember]
        [Description("Получатель сообщения")]
        public String Recipient { get; }
        /// <summary>
        /// Включить предыдущий результат после тела сообщения
        /// </summary>
        [DataMember]
        [Description("Включить предыдущий результат после тела сообщения")]
        public Boolean IncludePrevRes { get; }

        public SendMessageAct(String recipient, String body, EMessageType messageType = EMessageType.Email, Boolean includePrevRes = true)
        {
            if(String.IsNullOrEmpty(recipient))
                throw new ArgumentNullException(nameof(recipient));
            MessageType = messageType;
            Recipient = recipient;
            Body = body;
            IncludePrevRes = includePrevRes;
        }

        /// <summary>Возвращает строку, представляющую текущий объект.</summary>
        /// <returns>Строка, представляющая текущий объект.</returns>
        public override string ToString()
        {
            return this.GetString();
        }
    }
}
