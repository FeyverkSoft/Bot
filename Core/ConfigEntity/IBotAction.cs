﻿using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using Core.ConfigEntity.ActionObjects;

namespace Core.ConfigEntity
{
    public interface IBotAction: INotifyPropertyChanged
    {
        /// <summary>
        /// Указывает на то что в текущий момент выполняется это действие.
        /// </summary>
        [IgnoreDataMember]
        Boolean IsCurrent { get; set; }
        /// <summary>
        /// Тип события
        /// </summary>
        [DataMember]
        ActionType ActionType { get; }

        /// <summary>
        /// Описание действий для данного события
        /// </summary>
        [DataMember]
        ListAct SubActions { get; }
        /// <summary>
        /// Провалидировать массив событий
        /// </summary>
        /// <returns></returns>
        [IgnoreDataMember]
        Boolean IsValid { get; }

        /// <summary>
        /// Указавает поддерживается ли множественные действия или нет
        /// </summary>
        /// <returns></returns>
        [IgnoreDataMember]
        Boolean IsMultiAct { get; }
    }
}