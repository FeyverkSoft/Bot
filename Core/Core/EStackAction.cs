using Core.Attributes;

namespace Core.Core
{
    [LocDescription("EStackAction")]
    public enum EStackAction
    {
        [LocDescription("EStackAction_Pop")]
        Pop,
        [LocDescription("EStackAction_Push")]
        Push,
        [LocDescription("EStackAction_Peek")]
        Peek,
        [LocDescription("EStackAction_Clear")]
        Clear
    }
}
