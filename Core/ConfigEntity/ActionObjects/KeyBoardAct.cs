using System;
using System.ComponentModel;
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
    [Description("Событие нажатия клавишы на клавиатуре.")]
    public sealed class KeyBoardAct : IAction
    {
        /// <summary>
        /// Клавиша, нажатие которой надо эмулировать
        /// </summary>
        [DataMember]
        [Description("Клавиша, нажатие которой надо эмулировать")]
        public KeyCode Key { get; private set; }
        /// <summary>
        /// Время удержания клавиши
        /// </summary>
        [DataMember]
        [Description("Время удержания клавиши")]
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
