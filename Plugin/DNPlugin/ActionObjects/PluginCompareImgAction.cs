using System;
using System.Runtime.Serialization;
using CommonLib.Attributes;
using Core.ConfigEntity;
using Core.ConfigEntity.ActionObjects;

namespace ImgComparer.ActionObjects
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
        [ControlType("FilePath")]
        public String SamplePath { get; set; }

        public PluginCompareImgAction()
        {
            
        }
    }
}
