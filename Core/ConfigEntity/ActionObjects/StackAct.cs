using System.Runtime.Serialization;
using CommonLib.Attributes;
using Core.Core;

namespace Core.ConfigEntity.ActionObjects
{
    [LocDescription("StackAct", typeof(Resources.CoreText))]
    public class StackAct : BaseActionObject
    {
        /// <summary>
        /// Тип действия для внутренней фабрики
        /// </summary>
        [IgnoreDataMember]
        public new static ActionType ActionType => ActionType.Stack;

        [LocDescription("StackAct_Action", typeof(Resources.CoreText))]
        public EStackAction Action { get; set; }

        public StackAct(EStackAction action)
        {
            Action = action;
        }

        public StackAct() { }
    }
}
