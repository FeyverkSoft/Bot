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
        Contained = 2,
        /// <summary>
        /// Начинается с указанного набора
        /// </summary>
        Start = 4,
        /// <summary>
        /// Заканчивается указанным набором
        /// </summary>
        End = 8
    }
}
