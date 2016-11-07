using CommonLib.Attributes;

namespace Core.Core
{
    [LocDescription("EStackAction", typeof(Resources.CoreText))]
    public enum EStackAction
    {
        [LocDescription("EStackAction_Pop", typeof(Resources.CoreText))]
        Pop,
        [LocDescription("EStackAction_Push", typeof(Resources.CoreText))]
        Push,
        [LocDescription("EStackAction_Peek", typeof(Resources.CoreText))]
        Peek,
        [LocDescription("EStackAction_Clear", typeof(Resources.CoreText))]
        Clear
    }
}
