﻿using System;
using System.Runtime.Serialization;
using CommonLib.Attributes;
using Core.Helpers;

namespace Core.ConfigEntity.ActionObjects
{
    [LocDescription("GoToAct", typeof(Resources.CoreText))]
    public class GoToAct : BaseActionObject
    {
        /// <summary>
        /// Тип действия для внутренней фабрики
        /// </summary>
        [IgnoreDataMember]
        public new static ActionType ActionType => ActionType.GOTO;

        [LocDescription("GoToAct_LabelName", typeof(Resources.CoreText))]
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
