using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Core.ConfigEntity.ActionObjects
{
    /// <summary>
    /// Описание параметров ожидания появления окна с нужным названием.
    /// </summary>
    [DataContract]
    public class ExpectWindowAct : IAction
    {
        /// <summary>
        /// Наименование ожидаемого окна
        /// </summary>
        [DataMember]
        public String WinTitle { get; private set; }
        /// <summary>
        /// Указывает, на то что после того как окно найденно, ему необходимо передать фокус
        /// </summary>
        [DataMember]
        public Boolean SetFocus { get; private set; } = true;

        [JsonConstructor]
        public ExpectWindowAct(String winTitle, Boolean setFocus = true)
        {
            Debug.WriteLine($"{GetType().Name}.ctor->(winTitle:{winTitle}; SetFocus:{setFocus})");
            if (String.IsNullOrEmpty(winTitle))
                throw new ArgumentOutOfRangeException(nameof(winTitle));

            WinTitle = winTitle;
            SetFocus = setFocus;
        }
    }
}
