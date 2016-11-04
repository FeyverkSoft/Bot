using System;
using System.Runtime.Serialization;
using Core.Attributes;
using Core.Core;

namespace Core.ConfigEntity.ActionObjects
{
    [LocDescription("StackAct")]
    public class StackAct : BaseActionObject
    {
        /// <summary>
        /// Тип действия для внутренней фабрики
        /// </summary>
        [IgnoreDataMember]
        public new static ActionType ActionType => ActionType.Stack;

        [LocDescription("StackAct_Action")]
        public EStackAction Action { get; set; }

        public StackAct(EStackAction action)
        {
            Action = action;
        }

        public StackAct() { }
    }
}
