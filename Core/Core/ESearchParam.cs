using System;
using Core.Attributes;

namespace Core.Core
{
    /// <summary>
    /// Параметры поиска
    /// </summary>
    [Flags]
    [LocDescription("ESearchParam")]
    public enum ESearchParam
    {
        /// <summary>
        /// Содержит указанный набор
        /// </summary>
        [LocDescription("ESearchParam_Contained")]
        Contained = 0x0,
        /// <summary>
        /// Начинается с указанного набора
        /// </summary>
        [LocDescription("ESearchParam_Start")]
        Start = 0x1,
        /// <summary>
        /// Заканчивается указанным набором
        /// </summary>
        [LocDescription("ESearchParam_End")]
        End = 0x2
    }
}
