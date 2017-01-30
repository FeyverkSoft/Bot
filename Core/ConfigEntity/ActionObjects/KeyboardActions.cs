using System;
using System.Runtime.Serialization;
using CommonLib.Attributes;
using Core.Core;
using Core.Handlers.KeyBoard;
using LogWrapper;
using Newtonsoft.Json;

namespace Core.ConfigEntity.ActionObjects
{
    /// <summary>
    /// Событие клавиатуры
    /// </summary>
    [DataContract]
    [LocDescription("KeyboardAct", typeof(Resources.CoreText))]
    public class KeyboardAct : BaseActionObject
    {
        /// <summary>
        /// Тип действия для внутренней фабрики
        /// </summary>
        [IgnoreDataMember]
        public new static ActionType ActionType => ActionType.KeyBoardAction;

        /// <summary>
        /// Клавиша, нажатие которой надо эмулировать
        /// </summary>
        [DataMember]
        [LocDescription("KeyBoardAct_Key", typeof(Resources.CoreText))]
        public KeyName Key { get; set; }

        /// <summary>
        /// Клавиша, нажатие которой надо эмулировать
        /// </summary>
        [DataMember]
        [LocDescription("KeyboardAct_KeyAction", typeof(Resources.CoreText))]
        public KeyAction KeyAction { get; set; }

        [JsonConstructor]
        public KeyboardAct(KeyName key, KeyAction action)
        {
            Log.WriteLine($"{GetType().Name}.ctor->(key:{key};)");
            Key = key;
            KeyAction = action;
        }
        public KeyboardAct(){}
    }
}
