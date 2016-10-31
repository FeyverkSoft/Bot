using System;
using System.Runtime.Serialization;
using Core.Attributes;
using Core.Core;
using Core.Helpers;
using LogWrapper;
using Newtonsoft.Json;

namespace Core.ConfigEntity.ActionObjects
{
    /// <summary>
    /// Событие нажатия клавишы на клавиатуре.
    /// </summary>
    [DataContract]
    [LocDescription("KeyBoardAct")]
    public sealed class KeyBoardAct : BaseActionObject
    {
        /// <summary>
        /// Клавиша, нажатие которой надо эмулировать
        /// </summary>
        [DataMember]
        [LocDescription("KeyBoardAct_Key")]
        public KeyCode Key { get; private set; }
        /// <summary>
        /// Время удержания клавиши
        /// </summary>
        [DataMember]
        [LocDescription("KeyBoardAct_Time")]
        public UInt32 Time { get; private set; }

        [JsonConstructor]
        public KeyBoardAct(KeyCode key, UInt32 time = 0)
        {
            Log.WriteLine($"{GetType().Name}.ctor->(key:{key};)");
            Key = key;
            Time = time;
        }

        public KeyBoardAct() { }
    }
}
