using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Core.ConfigEntity
{
    public interface IBotAction: INotifyPropertyChanged, IActionsContainer
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
        /// Провалидировать массив событий
        /// </summary>
        /// <returns></returns>
        [IgnoreDataMember]
        Boolean IsValid { get; }
    }
}