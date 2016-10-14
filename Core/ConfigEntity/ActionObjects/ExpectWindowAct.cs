using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using Core.Core;
using LogWrapper;
using Newtonsoft.Json;

namespace Core.ConfigEntity.ActionObjects
{
    /// <summary>
    /// Описание параметров ожидания появления окна с нужным названием.
    /// </summary>
    [DataContract]
    [Description("Описание параметров ожидания появления окна с нужным названием.")]
    public class ExpectWindowAct : IAction
    {
        /// <summary>
        /// Наименование ожидаемого окна
        /// </summary>
        [DataMember]
        [Description("Наименование ожидаемого окна")]
        public String WinTitle { get; private set; }
        /// <summary>
        /// Указывает, на то что после того как окно найденно, ему необходимо передать фокус
        /// </summary>
        [DataMember]
        [Description("Указывает, на то что после того как окно найденно, ему необходимо передать фокус")]
        public Boolean SetFocus { get; private set; }
        /// <summary>
        /// Параметр поиска, указавающий как и где искать. в начале, конце или просто содержание
        /// </summary>
        [Description("Параметр поиска, указавающий как и где искать. в начале, конце или просто содержание")]
        public ESearchParam SearchParam { get; private set; } = ESearchParam.Contained;
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
    }
}
