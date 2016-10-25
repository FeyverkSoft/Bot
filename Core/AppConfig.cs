using System;
using Core.ConfigEntity;

namespace Core
{
    /// <summary>
    /// Текущий конфиг ядра
    /// Сингелтон в принципе
    /// </summary>
    public class AppConfig
    {
        private static readonly IConfigReader<AppConfig> AppConfigReader;
        private static String _path = "CoreConfig.json";

        static AppConfig()
        {
            if (AppConfigReader != null)
                return;
            AppConfigReader = ConfigReaderFactory.Get<AppConfig>();
            LoadInstance();
        }

        public static AppConfig Instance { get; private set; }

        /// <summary>
        /// Номер порта для доставки SmtpPort
        /// </summary>
        public Int32 SmtpPort { get; set; }

        /// <summary>
        /// Имя хоста доставки для email
        /// </summary>
        public String SmtpHost { get; set; }

        /// <summary>
        /// логин к почтовому аккаунту с которого будет производиться рассылка
        /// </summary>
        public String SmtpLogin { get; set; }

        /// <summary>
        /// Пароль к почтовому аккаунту с которого будет производиться рассылка
        /// </summary>
        public String SmtpPassword { get; set; }

        /// <summary>
        /// Использовать ядро по умолчанию?
        /// </summary>
        public Boolean DefaultCore { get; set; } = true;
        /// <summary>
        /// Загружать плагины?
        /// </summary>
        public Boolean LoadPlugin { get; set; } = true;

        public static void LoadInstance()
        {
            Instance = AppConfigReader.Load(_path);
        }
        public static void SaveInstance()
        {
            AppConfigReader.Save(Instance, _path);
        }

        public AppConfig()
        {

        }
    }
}
