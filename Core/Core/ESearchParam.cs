using System;

namespace Core.Core
{
    /// <summary>
    /// Параметры поиска
    /// </summary>
    [Flags]
    public enum ESearchParam
    {
        /// <summary>
        /// Содержит указанный набор
        /// </summary>
        Contained = 0x0,
        /// <summary>
        /// Начинается с указанного набора
        /// </summary>
        Start = 0x1,
        /// <summary>
        /// Заканчивается указанным набором
        /// </summary>
        End = 0x2
    }
}
