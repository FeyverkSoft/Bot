using System;
using System.Runtime.Serialization;
using CommonLib.Attributes;
using Core.Core;
using Newtonsoft.Json;

namespace Core.ConfigEntity.ActionObjects
{
    /// <summary>
    /// Событие нажатия клавишы на клавиатуре.
    /// </summary>
    [DataContract]
    [LocDescription("KeyBoardKeysAct", typeof(Resources.CoreText))]
    public sealed class KeyBoardKeysAct : KeyBoardAct
    {
        /// <summary>
        /// Тип действия для внутренней фабрики
        /// </summary>
        [IgnoreDataMember]
        public new static ActionType ActionType => ActionType.KeyBoardShortcut;

        [JsonConstructor]
        public KeyBoardKeysAct(KeyName key, UInt32 time = 0):base(key, time)
        {
        }

        public KeyBoardKeysAct() { }
    }
}
