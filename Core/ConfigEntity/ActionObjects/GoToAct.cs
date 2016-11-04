using System;
using Core.Attributes;
using Core.Helpers;

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

        /// <summary>Возвращает строку, представляющую текущий объект.</summary>
        /// <returns>Строка, представляющая текущий объект.</returns>
        public override String ToString()
        {
            return this.GetString();
        }
    }
}
