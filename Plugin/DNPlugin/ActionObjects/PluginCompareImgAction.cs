using System;
using System.Runtime.Serialization;
using Core.ConfigEntity;
using Core.ConfigEntity.ActionObjects;

namespace DNPlugin.ActionObjects
{
    [DataContract]
    public class PluginCompareImgAction : BaseActionObject
    {
        /// <summary>
        /// Тип действия для внутренней фабрики
        /// </summary>
        [IgnoreDataMember]
        public new static ActionType ActionType => ActionType.PluginAct;

        public PluginCompareImgAction(String samplePath)
        {
            SamplePath = samplePath;
        }
        [DataMember]
        public String SamplePath { get; set; }

        /// <summary>
        /// Процен после которого происходит срабатывание 
        /// </summary>
        [DataMember]
        public Int32 Procent { get; set; }
        public PluginCompareImgAction()
        {
            
        }
    }
}
