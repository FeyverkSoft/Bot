using System;
using System.ComponentModel;
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
    [LocDescription("Событие нажатия клавишы на клавиатуре.")]
    public sealed class KeyBoardAct : IAction
    {
        /// <summary>
        /// Клавиша, нажатие которой надо эмулировать
        /// </summary>
        [DataMember]
        [LocDescription("Клавиша, нажатие которой надо эмулировать")]
        public KeyCode Key { get; private set; }
        /// <summary>
        /// Время удержания клавиши
        /// </summary>
        [DataMember]
        [LocDescription("Время удержания клавиши")]
        public UInt32 Time { get; private set; }

        [JsonConstructor]
        public KeyBoardAct(KeyCode key, UInt32 time = 0)
        {
            Log.WriteLine($"{GetType().Name}.ctor->(key:{key};)");
            Key = key;
            Time = time;
        }

        /// <summary>Возвращает строку, представляющую текущий объект.</summary>
        /// <returns>Строка, представляющая текущий объект.</returns>
        public override string ToString()
        {
            return this.GetString();
        }
    }
}
