using System.Diagnostics;
using System.Runtime.Serialization;
using Core.Core;
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

        [JsonConstructor]
        public KeyBoardAct(KeyCode key)
        {
            Debug.WriteLine($"{GetType().Name}.ctor->(key:{key};)");
            Key = key;
        }
    }
}
