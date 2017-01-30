using System;
using System.Runtime.Serialization;
using CommonLib.Attributes;
using LogWrapper;
using Newtonsoft.Json;

namespace Core.ConfigEntity.ActionObjects
{
    /// <summary>
    /// Описание параметров для вызова внешнего по
    /// </summary>
    [DataContract]
    [LocDescription("RunAct", typeof(Resources.CoreText))]
    public class RunAct : BaseActionObject
    {
        /// <summary>
        /// Тип действия для внутренней фабрики
        /// </summary>
        [IgnoreDataMember]
        public new static ActionType ActionType => ActionType.Run;

        /// <summary>
        /// Имя процесса который надо запустить
        /// </summary>
        [DataMember]
        [LocDescription("RunAct_ProcessName", typeof(Resources.CoreText))]
        public String ProcessName { get; set; }
        /// <summary>
        /// Ключи запуска процесса
        /// </summary>
        [DataMember]
        [LocDescription("RunAct_ProcessKey", typeof(Resources.CoreText))]
        public String ProcessKey { get; set; }

        [JsonConstructor]
        public RunAct(String processName, String processKey = "")
        {
            Log.WriteLine($"{GetType().Name}.ctor->(ProcessName:{processName}; ProcessKey: {processKey})");
            if (String.IsNullOrEmpty(processName))
                throw new ArgumentOutOfRangeException(nameof(processName));

            ProcessName = processName;
            ProcessKey = processKey;
        }

        public RunAct() { }
    }
}
