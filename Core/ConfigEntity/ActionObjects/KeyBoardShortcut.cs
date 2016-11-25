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
    [LocDescription("KeyBoardShortcut", typeof(Resources.CoreText))]
    public sealed class KeyBoardShortcut : BaseActionObject
    {
        /// <summary>
        /// Тип действия для внутренней фабрики
        /// </summary>
        [IgnoreDataMember]
        public new static ActionType ActionType => ActionType.KeyBoardShortcut;

        /// <summary>
        /// Клавиша, нажатие которой надо эмулировать
        /// </summary>
        [DataMember]
        [LocDescription("KeyBoardAct_Key", typeof(Resources.CoreText))]
        public KeyName Key { get; set; }

        [JsonConstructor]
        public KeyBoardShortcut(KeyName key)
        {
            Key = key;
        }

        public KeyBoardShortcut() { }
    }
}
