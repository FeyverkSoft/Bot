using System;
using System.Runtime.Serialization;
using Core.Attributes;
using Core.Core;
using LogWrapper;
using Newtonsoft.Json;

namespace Core.ConfigEntity.ActionObjects
{
    /// <summary>
    /// Описание параметров ожидания появления окна с нужным названием.
    /// </summary>
    [DataContract]
    [LocDescription("ExpectWindowActDescription")]
    public class ExpectWindowAct : BaseActionObject
    {
        /// <summary>
        /// Тип действия для внутренней фабрики
        /// </summary>
        [IgnoreDataMember]
        public new static ActionType ActionType => ActionType.ExpectWindow;
        /// <summary>
        /// Наименование ожидаемого окна
        /// </summary>
        [DataMember]
        [LocDescription("ExpectWindowAct_WinTitle")]
        public String WinTitle { get; set; }
        /// <summary>
        /// Указывает, на то что после того как окно найденно, ему необходимо передать фокус
        /// </summary>
        [DataMember]
        [LocDescription("ExpectWindowAct_SetFocus")]
        public Boolean SetFocus { get; set; }
        /// <summary>
        /// Параметр поиска, указавающий как и где искать. в начале, конце или просто содержание
        /// </summary>
        [LocDescription("ExpectWindowAct_SearchParam")]
        public ESearchParam SearchParam { get; set; } = ESearchParam.Contained;
        [JsonConstructor]
        public ExpectWindowAct(String winTitle, Boolean setFocus = true)
        {
            Log.WriteLine($"{GetType().Name}.ctor->(winTitle:{winTitle}; SetFocus:{setFocus})");
            if (String.IsNullOrEmpty(winTitle))
                throw new ArgumentOutOfRangeException(nameof(winTitle));

            WinTitle = winTitle;
            SetFocus = setFocus;
        }

        public ExpectWindowAct(String winTitle, ESearchParam searchParam = ESearchParam.Contained, Boolean setFocus = true) : this(winTitle, setFocus)
        {
            Log.WriteLine($"{GetType().Name}.ctor->(winTitle:{winTitle}; SetFocus:{setFocus}); SearchParam: {searchParam}");
            SearchParam = searchParam;
        }

        public ExpectWindowAct() { }
    }
}
