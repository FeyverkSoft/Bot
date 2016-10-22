using System;
using System.Collections.Generic;
using System.Globalization;

namespace WpfExecutor.Extensions.Localization
{
    /// <summary>
    /// Интерфейс для реализации поставщика локализованных строк
    /// </summary>
    public interface ILocalizationProvider
    {
        /// <summary>
        /// Возвращает локализованный объект по ключу
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <returns></returns>
        object Localize(String key);

        /// <summary>
        /// Возвращает локализованный объект по привязки
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <returns></returns>
        object Localize(Object key);

        /// <summary>
        /// Доступные культуры
        /// </summary>
        IEnumerable<CultureInfo> Cultures { get; }
    }
}
