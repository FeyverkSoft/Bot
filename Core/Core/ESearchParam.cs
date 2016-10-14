using System;
using System.ComponentModel;

namespace Core.Core
{
    /// <summary>
    /// Параметры поиска
    /// </summary>
    [Flags]
    [Description("Параметры поиска")]
    public enum ESearchParam
    {
        /// <summary>
        /// Содержит указанный набор
        /// </summary>
        [Description("одержит указанный набор")]
        Contained = 0x0,
        /// <summary>
        /// Начинается с указанного набора
        /// </summary>
        [Description("Начинается с указанного набора")]
        Start = 0x1,
        /// <summary>
        /// Заканчивается указанным набором
        /// </summary>
        [Description("Заканчивается указанным набором")]
        End = 0x2
    }
}
