using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using CommonLib.Attributes;
using LogWrapper;
using Newtonsoft.Json;

namespace Core.ConfigEntity.ActionObjects
{
    /// <summary>
    /// Действие цикла
    /// </summary>
    [DataContract]
    [LocDescription("LoopAct", typeof(Resources.CoreText))]
    public class LoopAct : BaseActionObject, IBotActionContainer
    {
        /// <summary>
        /// Тип действия для внутренней фабрики
        /// </summary>
        [IgnoreDataMember]
        public new static ActionType ActionType => ActionType.Loop;

        /// <summary>
        /// Колличество выполнений цикла
        /// </summary>
        [DataMember]
        [LocDescription("LoopAct_IterationCount", typeof(Resources.CoreText))]
        public Int32 IterationCount { get; set; } = 1;
        /// <summary>
        /// Список действий которые необходимо выполнить в цикле
        /// </summary>
        [DataMember]
        [LocDescription("LoopAct_Actions", typeof(Resources.CoreText))]
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
        public LoopAct(Int32 iterationCount = 0, ListBotAction actions = null)
        {
            Log.WriteLine($"{GetType().Name}.ctor->(iterationCount:{iterationCount}; actions: {actions?.Count ?? -1})");
            if (iterationCount < 0)
                throw new ArgumentOutOfRangeException(nameof(iterationCount));
            if (actions != null)
                Actions = actions;
        }
        public LoopAct(){}
    }
}
