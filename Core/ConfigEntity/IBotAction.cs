﻿using System;
using System.Runtime.Serialization;
using Core.ConfigEntity.ActionObjects;

namespace Core.ConfigEntity
{
    public interface IBotAction
    {
        /// <summary>
        /// Тип события
        /// </summary>
        [DataMember]
        ActionType ActionType { get; }

        /// <summary>
        /// Описание действий для данного события
        /// </summary>[DataMember]
        ListAction SubActions { get; }
        /// <summary>
        /// Провалидировать массив событий
        /// </summary>
        /// <returns></returns>
        [IgnoreDataMember]
        Boolean IsValid { get; }

    }
}