using System;
using CommonLib.Attributes;

namespace Core.Core
{
    /// <summary>
    /// Параметры поиска
    /// </summary>
    [Flags]
    [LocDescription("ESearchParam", typeof(Resources.CoreText))]
    public enum ESearchParam
    {
        /// <summary>
        /// Содержит указанный набор
        /// </summary>
        [LocDescription("ESearchParam_Contained", typeof(Resources.CoreText))]
        Contained = 0x0,
        /// <summary>
        /// Начинается с указанного набора
        /// </summary>
        [LocDescription("ESearchParam_Start", typeof(Resources.CoreText))]
        Start = 0x1,
        /// <summary>
        /// Заканчивается указанным набором
        /// </summary>
        [LocDescription("ESearchParam_End", typeof(Resources.CoreText))]
        End = 0x2
    }
}
