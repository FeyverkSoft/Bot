﻿using System;
using System.Collections.Generic;
using Core;
using Core.ConfigEntity;
using Core.Core;
using WpfExecutor.Extensions.Localization;
using WpfExecutor.Extensions.Provider;

namespace WpfExecutor
{
    /// <summary>
    /// Контекст для инжектирования в приложении
    /// </summary>
    public static class AppContext
    {
        /// <summary>
        /// Список сингелтонов в рамках приложения
        /// </summary>
        private static readonly Dictionary<Type, Object> Dictionary = new Dictionary<Type, Object>();

        static AppContext()
        {
            Dictionary.Add(typeof(ILocalizationProvider), new ResxLocalizationProvider());
            Dictionary.Add(typeof(IExecutiveCore), CoreFactory.GetCore());
            Dictionary.Add(typeof(IConfigReader<Config>), ConfigReaderFactory.Get<Config>());
        }

        public static T Get<T>() where T : class
        {
            return (T)Dictionary[typeof(T)];
        }

        public static void Set<T>(T inst)
        {
            if (!Dictionary.ContainsKey(typeof(T)))
            {
                Dictionary.Add(typeof(T), inst);
            }
            else
            {
                Dictionary[typeof(T)] = inst;
            }
        }
    }
}
