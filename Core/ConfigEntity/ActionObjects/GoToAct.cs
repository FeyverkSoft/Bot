using System;
using Core.Attributes;

namespace Core.ConfigEntity.ActionObjects
{
    [LocDescription("GoToAct")]
    public class GoToAct : BaseActionObject
    {
        [LocDescription("GoToAct_LabelName")]
        public String LabelName { get; set; }

        public GoToAct(String labelName)
        {
            LabelName = labelName;
        }

        public GoToAct() { }
    }
}
