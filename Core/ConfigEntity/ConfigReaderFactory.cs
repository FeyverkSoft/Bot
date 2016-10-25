using System;
using System.Collections.Generic;

namespace Core.ConfigEntity
{
    public static class ConfigReaderFactory
    {
        /// <summary>
        /// Список сингелтонов
        /// </summary>
        private static readonly Dictionary<Type, Object> Dictionary = new Dictionary<Type, Object>();

        static ConfigReaderFactory()
        {
            Dictionary.Add(typeof(AppConfig), new ConfigReader<AppConfig>(false, false, false));
        }

        public static IConfigReader<T> Get<T>() where T : class, new()
        {
            if (Dictionary.ContainsKey(typeof(T)))
                return (IConfigReader<T>)Dictionary[typeof(T)];
            switch (typeof(T).Name)
            {
                case nameof(Config):
                    return new ConfigReader<Config>() as IConfigReader<T>;
            }
            throw new NotSupportedException(nameof(T));
        }

        public static void Set<T>(IConfigReader<T> inst) where T : class, new()
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
