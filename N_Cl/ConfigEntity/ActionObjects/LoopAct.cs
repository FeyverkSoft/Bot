using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Executor.ConfigEntity.ActionObjects
{
    /// <summary>
    /// Действие цикла
    /// </summary>
    [DataContract]
    public class LoopAct : IAction
    {
        /// <summary>
        /// Колличество выполнений цикла
        /// </summary>
        [DataMember]
        public Int32 IterationCount { get; private set; } = 1;
        /// <summary>
        /// Список действий которые необходимо выполнить в цикле
        /// </summary>
        [DataMember]
        public ListBotAction Actions { get; set; } = new ListBotAction();

        [JsonConstructor]
        public LoopAct(Int32 iterationCount, List<BotAction> actions)
        {
            Debug.WriteLine($"{GetType().Name}.ctor->(iterationCount:{iterationCount}; actions: {actions?.Count ?? -1})");
            if (iterationCount < 0)
                throw new ArgumentOutOfRangeException(nameof(iterationCount));
            if (actions != null)
                Actions = actions;
        }
        public LoopAct(Int32 iterationCount, ListBotAction actions)
        {
            Debug.WriteLine($"{GetType().Name}.ctor->(iterationCount:{iterationCount}; actions: {actions?.Count ?? -1})");
            if (iterationCount < 0)
                throw new ArgumentOutOfRangeException(nameof(iterationCount));
            if (actions != null)
                Actions = actions;
        }
    }
}
