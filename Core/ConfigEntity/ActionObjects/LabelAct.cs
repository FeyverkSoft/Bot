using System;
using Core.Attributes;

namespace Core.ConfigEntity.ActionObjects
{
    [LocDescription("LabelAct")]
    public class LabelAct : BaseActionObject
    {
        [LocDescription("LabelAct_LabelName")]
        public String LabelName { get; set; }

        public LabelAct(String labelName)
        {
            LabelName = labelName;
        }

        public LabelAct() { }
    }
}
