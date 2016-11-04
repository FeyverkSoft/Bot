using System;
using System.Runtime.Serialization;
using Core.ConfigEntity;
using Core.ConfigEntity.ActionObjects;
using Newtonsoft.Json;

namespace TestPlugin
{
    [DataContract]
    public class TestAct : BaseActionObject
    {
        /// <summary>
        /// Тип действия для внутренней фабрики
        /// </summary>
        [IgnoreDataMember]
        public new static ActionType ActionType => ActionType.PluginAct;

        [JsonConstructor]
        public TestAct(String test)
        {
            Test = test;
        }
        [DataMember]
        public String Test { get; private set; }
    }
}
