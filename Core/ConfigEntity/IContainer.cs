using System.Runtime.Serialization;
using Core.ConfigEntity.ActionObjects;

namespace Core.ConfigEntity
{
    public interface IBotActionContainer
    {
        ListBotAction Actions { get; }
    }
    public interface IActionsContainer
    {
        /// <summary>
        /// Описание действий для данного события
        /// </summary>
        [DataMember]
        ListAct SubActions { get; }
    }
}
