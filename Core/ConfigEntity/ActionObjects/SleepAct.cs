﻿using System;
using System.Runtime.Serialization;
using LogWrapper;
using Newtonsoft.Json;

namespace Core.ConfigEntity.ActionObjects
{
    /// <summary>
    /// Описание параметров для сна, перед выполнением новой комманды
    /// </summary>
    [DataContract]
    public class SleepAct : IAction
    {
        /// <summary>
        /// Время на сколько процесс должен заснуть
        /// </summary>
        [DataMember]
        public Int32 Delay { get; private set; } = 2;
        /// <summary>
        /// Верхняя граница дополнительной, случайно задержки
        /// </summary>
        [DataMember]
        public Int32 MaxRandDelay { get; private set; } = 0;
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
    }
}