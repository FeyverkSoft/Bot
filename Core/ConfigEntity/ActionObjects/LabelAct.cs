using System;
using System.Runtime.Serialization;
using CommonLib.Attributes;

namespace Core.ConfigEntity.ActionObjects
{
    [LocDescription("LabelAct", typeof(Resources.CoreText))]
    public class LabelAct : BaseActionObject
    {
        /// <summary>
        /// Тип действия для внутренней фабрики
        /// </summary>
        [IgnoreDataMember]
        public new static ActionType ActionType => ActionType.Label;

        [LocDescription("LabelAct_LabelName", typeof(Resources.CoreText))]
        public String LabelName { get; set; }

        public LabelAct(String labelName)
        {
            LabelName = labelName;
        }

        public LabelAct() { }
    }
}
