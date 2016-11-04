using System;
using System.Runtime.Serialization;
using Core.Attributes;

namespace Core.ConfigEntity.ActionObjects
{
    [LocDescription("LabelAct")]
    public class LabelAct : BaseActionObject
    {
        /// <summary>
        /// Тип действия для внутренней фабрики
        /// </summary>
        [IgnoreDataMember]
        public new static ActionType ActionType => ActionType.Label;

        [LocDescription("LabelAct_LabelName")]
        public String LabelName { get; set; }

        public LabelAct(String labelName)
        {
            LabelName = labelName;
        }

        public LabelAct() { }
    }
}
