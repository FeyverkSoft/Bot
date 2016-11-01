using System;
using System.Runtime.Serialization;
using Core.ConfigEntity.ActionObjects;
using Newtonsoft.Json;

namespace TestPlugin
{
    [DataContract]
    public class TestAct : IAction
    {
        [JsonConstructor]
        public TestAct(String test)
        {
            Test = test;
        }
        [DataMember]
        public String Test { get; private set; }
    }
}
