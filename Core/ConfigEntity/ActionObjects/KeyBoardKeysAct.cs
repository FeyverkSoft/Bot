using System;
using System.Runtime.Serialization;
using Core.Attributes;
using Core.Core;
using Newtonsoft.Json;

namespace Core.ConfigEntity.ActionObjects
{
    /// <summary>
    /// Событие нажатия клавишы на клавиатуре.
    /// </summary>
    [DataContract]
    [LocDescription("KeyBoardKeysAct")]
    public sealed class KeyBoardKeysAct : KeyBoardAct
    {
        [JsonConstructor]
        public KeyBoardKeysAct(KeyCode key, UInt32 time = 0):base(key, time)
        {
        }

        public KeyBoardKeysAct() { }
    }
}
