using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
