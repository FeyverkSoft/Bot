using System;
using System.Runtime.Serialization;
using LogWrapper;
using Newtonsoft.Json;

namespace Core.ConfigEntity.ActionObjects
{
    /// <summary>
    /// Описание параметров для сна, перед выполнением новой комманды
    /// </summary>
    [DataContract]
    public class IfAction : IAction
    {
        /// <summary>
        /// Список действий которые необходимо выполнить при успешном выполнении условия
        /// </summary>
        [DataMember]
        public ListBotAction Actions { get; private set; } = new ListBotAction();

        /// <summary>
        /// Список действий которые необходимо выполнить при НЕ успешном выполнении условия
        /// </summary>
        [DataMember]
        public ListBotAction FailActions { get; private set; } = new ListBotAction();

        /// <summary>
        /// Список условий, которые необходимо проверить над предыдущим объектом
        /// </summary>
        [DataMember]
        public String Сonditions { get; private set; }

        [JsonConstructor]
        public IfAction(String сonditions, ListBotAction actions, ListBotAction failActions)
        {
            Сonditions = сonditions;
            Log.WriteLine($"{GetType().Name}.ctor->(actions: {actions?.Count ?? -1}; failActions: {failActions?.Count ?? -1})");
            if (actions != null)
                Actions = actions;
            if (failActions != null)
                FailActions = failActions;
            if (сonditions == null)
                throw new ArgumentNullException(nameof(сonditions));
        }
    }
}
