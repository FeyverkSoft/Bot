using System;
using System.Runtime.Serialization;
using Core.Attributes;
using Core.Message;

namespace Core.ConfigEntity.ActionObjects
{
    /// <summary>
    /// Действие отправки казанного сообщения на указанные параметры
    /// </summary>
    [DataContract]
    [LocDescription("SendMessageAct")]
    public class SendMessageAct : BaseActionObject
    {
        /// <summary>
        /// Тип действия для внутренней фабрики
        /// </summary>
        [IgnoreDataMember]
        public new static ActionType ActionType => ActionType.SendMessage;

        /// <summary>
        /// Тип сообщения, например сообщение на емайл
        /// </summary>
        [DataMember]
        [LocDescription("SendMessageAct_MessageType")]
        public EMessageType MessageType { get; set; }
        /// <summary>
        /// Тело сообщения
        /// </summary>
        [DataMember]
        [LocDescription("SendMessageAct_Body")]
        public String Body { get; set; }
        /// <summary>
        /// Получатель сообщения
        /// </summary>
        [DataMember]
        [LocDescription("SendMessageAct_Recipient")]
        public String Recipient { get; set; }
        /// <summary>
        /// Включить предыдущий результат после тела сообщения
        /// </summary>
        [DataMember]
        [LocDescription("SendMessageAct_IncludePrevRes")]
        public Boolean IncludePrevRes { get; set; }

        public SendMessageAct(String recipient, String body, EMessageType messageType = EMessageType.Email, Boolean includePrevRes = true)
        {
            if(String.IsNullOrEmpty(recipient))
                throw new ArgumentNullException(nameof(recipient));
            MessageType = messageType;
            Recipient = recipient;
            Body = body;
            IncludePrevRes = includePrevRes;
        }
        public SendMessageAct() { }
    }
}
