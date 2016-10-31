﻿using System;
using System.Runtime.Serialization;
using Core.Attributes;
using Core.Helpers;
using LogWrapper;
using Newtonsoft.Json;

namespace Core.ConfigEntity.ActionObjects
{
    /// <summary>
    /// Описание условий и что необходимо выполнить
    /// </summary>
    [DataContract]
    [LocDescription("IfAction")]
    public class IfAction : BaseActionObject
    {
        /// <summary>
        /// Список действий которые необходимо выполнить при успешном выполнении условия
        /// </summary>
        [DataMember]
        [LocDescription("IfAction_SuccessLabel")]
        public String SuccessLabel { get; private set; }

        /// <summary>
        /// Список действий которые необходимо выполнить при НЕ успешном выполнении условия
        /// </summary>
        [DataMember]
        [LocDescription("IfAction_FailLabel")]
        public String FailLabel { get; private set; }

        /// <summary>
        /// Проверка на тип предыдущего результата 
        /// </summary>
        [LocDescription("IfAction_PrevResType")]
        [DataMember]
        public String PrevResType { get; private set; }

        /// <summary>
        /// Список условий, которые необходимо проверить над предыдущим объектом
        /// </summary>
        [DataMember]
        [LocDescription("IfAction_Сonditions")]
        public String Сonditions { get; private set; }

        [JsonConstructor]
        public IfAction(String prevResType, String сonditions, String successLabel, String failLabel)
        {
            if(String.IsNullOrEmpty(prevResType))
                throw new ArgumentNullException(nameof(prevResType));
            PrevResType = prevResType;
            Сonditions = сonditions;
            Log.WriteLine($"{GetType().Name}.ctor->(successLabel: {successLabel}; failLabel: {failLabel})");
            if (successLabel != null)
                SuccessLabel = successLabel;
            if (failLabel != null)
                FailLabel = failLabel;
            if (сonditions == null)
                throw new ArgumentNullException(nameof(сonditions));
        }

        public IfAction() { }
    }
}
