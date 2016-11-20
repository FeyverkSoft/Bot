using System;
using System.Collections.Generic;
using System.Reflection;
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
        /// Список приоритетов исполнителей и плагинов
        /// </summary>
        public List<String> PrioritetList { get; set; }

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
        /// <summary>
        /// эмулировать некую случайную неточность при движении указателя мышки?
        /// </summary>
        public Boolean RandomMouse { get; set; } = false;

        public static void LoadInstance()
        {
            Instance = AppConfigReader.Load(_path);
            if (Instance.PrioritetList == null)
                Instance.PrioritetList = new List<String>();
            if (!Instance.PrioritetList.Contains(Assembly.GetExecutingAssembly().GetName().Name))
            {
                Instance.PrioritetList.Add(Assembly.GetExecutingAssembly().GetName().Name);
                AppConfigReader.Save(Instance, _path);
            }

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
