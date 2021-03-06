﻿using System;
using System.Runtime.Serialization;
using CommonLib.Attributes;
using LogWrapper;
using Newtonsoft.Json;

namespace Core.ConfigEntity.ActionObjects
{
    /// <summary>
    /// Описание параметров для сна, перед выполнением новой комманды
    /// </summary>
    [DataContract]
    [LocDescription("SleepAct", typeof(Resources.CoreText))]
    public class SleepAct : BaseActionObject
    {
        /// <summary>
        /// Тип действия для внутренней фабрики
        /// </summary>
        [IgnoreDataMember]
        public new static ActionType ActionType => ActionType.Sleep;

        /// <summary>
        /// Время на сколько процесс должен заснуть
        /// </summary>
        [DataMember]
        [LocDescription("SleepAct_Delay", typeof(Resources.CoreText))]
        public Int32 Delay { get; set; } = 2;
        /// <summary>
        /// Верхняя граница дополнительной, случайно задержки
        /// </summary>
        [DataMember]
        [LocDescription("SleepAct_MaxRandDelay", typeof(Resources.CoreText))]
        public Int32 MaxRandDelay { get; set; } = 0;
        [JsonConstructor]
        public SleepAct(Int32 delay, Int32 randDelay = 0)
        {
            Log.WriteLine($"{GetType().Name}.ctor->(delay:{delay}; randDelay: {randDelay})");
            if (delay < 0)
                throw new ArgumentOutOfRangeException(nameof(delay));
            if (randDelay < 0)
                throw new ArgumentOutOfRangeException(nameof(randDelay));

            Delay = delay;
            MaxRandDelay = randDelay;
        }

        public SleepAct() { }
    }
}
