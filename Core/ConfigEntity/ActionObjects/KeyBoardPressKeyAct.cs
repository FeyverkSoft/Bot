using System;
using System.Runtime.Serialization;
using CommonLib.Attributes;
using Core.Core;
using LogWrapper;
using Newtonsoft.Json;

namespace Core.ConfigEntity.ActionObjects
{
    /// <summary>
    /// Событие нажатия клавишы на клавиатуре.
    /// </summary>
    [DataContract]
    [LocDescription("KeyBoardPressKeyAct", typeof(Resources.CoreText))]
    public class KeyBoardPressKeyAct : BaseActionObject
    {
        /// <summary>
        /// Тип действия для внутренней фабрики
        /// </summary>
        [IgnoreDataMember]
        public new static ActionType ActionType => ActionType.KeyBoardPressKey;
        /// <summary>
        /// Клавиша, нажатие которой надо эмулировать
        /// </summary>
        [DataMember]
        [LocDescription("KeyBoardAct_Key", typeof(Resources.CoreText))]
        public KeyName Key { get; set; }
        /// <summary>
        /// Время удержания клавиши
        /// </summary>
        [DataMember]
        [LocDescription("KeyBoardPressKeyAct_Time", typeof(Resources.CoreText))]
        public UInt32 Time { get; set; }

        [JsonConstructor]
        public KeyBoardPressKeyAct(KeyName key, UInt32 time = 0)
        {
            Log.WriteLine($"{GetType().Name}.ctor->(key:{key};)");
            Key = key;
            Time = time;
        }

        public KeyBoardPressKeyAct() { }
    }
}
