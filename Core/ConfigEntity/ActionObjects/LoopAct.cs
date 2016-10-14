using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using LogWrapper;
using Newtonsoft.Json;

namespace Core.ConfigEntity.ActionObjects
{
    /// <summary>
    /// Действие цикла
    /// </summary>
    [DataContract]
    [Description("Действие цикла")]
    public class LoopAct : IAction
    {
        /// <summary>
        /// Колличество выполнений цикла
        /// </summary>
        [DataMember]
        [Description("Колличество выполнений цикла")]
        public Int32 IterationCount { get; private set; } = 1;
        /// <summary>
        /// Список действий которые необходимо выполнить в цикле
        /// </summary>
        [DataMember]
        [Description("Список действий которые необходимо выполнить в цикле")]
        public ListBotAction Actions { get; set; } = new ListBotAction();

        [JsonConstructor]
        public LoopAct(Int32 iterationCount, List<BotAction> actions)
        {
            Log.WriteLine($"{GetType().Name}.ctor->(iterationCount:{iterationCount}; actions: {actions?.Count ?? -1})");
            if (iterationCount < 0)
                throw new ArgumentOutOfRangeException(nameof(iterationCount));
            if (actions != null)
                Actions = actions;
        }
        public LoopAct(Int32 iterationCount, ListBotAction actions)
        {
            Log.WriteLine($"{GetType().Name}.ctor->(iterationCount:{iterationCount}; actions: {actions?.Count ?? -1})");
            if (iterationCount < 0)
                throw new ArgumentOutOfRangeException(nameof(iterationCount));
            if (actions != null)
                Actions = actions;
        }
    }
}
