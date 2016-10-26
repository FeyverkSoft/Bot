using System;
using System.Collections.Generic;
using System.Globalization;
using WpfExecutor.Extensions.Localization;

namespace WpfExecutor.Extensions.Provider
{
    /// <summary>
    /// Реализация поставщика локализованных строк через ресурсы приложения
    /// </summary>
    public class ResxLocalizationProvider : ILocalizationProvider
    {
        private IEnumerable<CultureInfo> _cultures;

        public object Localize(String key)
        {
            return Resources.Localization.ResourceManager.GetObject(key);
        }

        /// <summary>
        /// Возвращает локализованный объект по привязки
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <returns></returns>
        public object Localize(Object key)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<CultureInfo> Cultures => _cultures ?? (_cultures = new List<CultureInfo>
        {
            new CultureInfo("ru-RU"),
            new CultureInfo("en-US"),
        });
    }
}
