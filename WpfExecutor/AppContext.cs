using System;
using System.Collections.Generic;
using WpfExecutor.Extensions.Localization;

namespace WpfExecutor
{
    /// <summary>
    /// Контекст для инжектирования в приложении
    /// </summary>
    public static class AppContext
    {
        private static readonly Dictionary<Type, Object> Dictionary = new Dictionary<Type, Object>();

        static AppContext()
        {
            Dictionary.Add(typeof(ILocalizationProvider), new ResxLocalizationProvider());
        }

        public static T Get<T>() where T : class
        {
            return (T)Dictionary[typeof(T)];
        }
    }
}
