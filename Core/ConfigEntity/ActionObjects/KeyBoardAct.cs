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
    [LocDescription("KeyBoardAct", typeof(Resources.CoreText))]
    public class KeyBoardAct : BaseActionObject
    {
        /// <summary>
        /// Тип действия для внутренней фабрики
        /// </summary>
        [IgnoreDataMember]
        public new static ActionType ActionType => ActionType.KeyBoard;
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
        [LocDescription("KeyBoardAct_Time", typeof(Resources.CoreText))]
        public UInt32 Time { get; set; }

        [JsonConstructor]
        public KeyBoardAct(KeyName key, UInt32 time = 0)
        {
            Log.WriteLine($"{GetType().Name}.ctor->(key:{key};)");
            Key = key;
            Time = time;
        }

        public KeyBoardAct() { }
    }
}
