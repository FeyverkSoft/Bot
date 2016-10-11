using System;
using System.Runtime.Serialization;
using Core.Core;
using LogWrapper;
using Newtonsoft.Json;

namespace Core.ConfigEntity.ActionObjects
{
    /// <summary>
    /// Событие нажатия клавишы на клавиатуре.
    /// </summary>
    [DataContract]
    public sealed class KeyBoardAct : IAction
    {
        /// <summary>
        /// Клавиша, нажатие которой надо эмулировать
        /// </summary>
        [DataMember]
        public KeyCode Key { get; private set; }
        /// <summary>
        /// Время удержания клавиши
        /// </summary>
        [DataMember]
        public UInt32 Time { get; private set; }

        [JsonConstructor]
        public KeyBoardAct(KeyCode key, UInt32 time = 0)
        {
            Log.WriteLine($"{GetType().Name}.ctor->(key:{key};)");
            Key = key;
            Time = time;
        }
    }
}
