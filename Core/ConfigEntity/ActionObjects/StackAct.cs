using System;
using Core.Attributes;
using Core.Core;

namespace Core.ConfigEntity.ActionObjects
{
    [LocDescription("StackAct")]
    public class StackAct : BaseActionObject
    {
        [LocDescription("StackAct_Action")]
        public EStackAction Action { get; set; }

        public StackAct(EStackAction action)
        {
            Action = action;
        }

        public StackAct() { }
    }
}
