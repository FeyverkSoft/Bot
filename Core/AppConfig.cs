﻿using System;
using Core.ConfigEntity;

namespace Core
{
    /// <summary>
    /// Класс с конфигурацией ядра
    /// </summary>
    internal static class AppConfig
    {
        static readonly IConfigReader<InternalAppConfig> AppConfigReader;

        static AppConfig()
        {
            if (AppConfigReader != null)
                return;
            var temp = (AppConfigReader = ConfigReaderFactory.Get<InternalAppConfig>()).Load("CoreConfig.json");

            SmtpPort = temp.SmtpPort;
            SmtpHost = temp.SmtpHost;
            SmtpLogin = temp.SmtpLogin;
            SmtpPassword = temp.SmtpPassword;
            DefaultCore = temp.DefaultCore;
            LoadPlugin = temp.LoadPlugin;
        }

        /// <summary>
        /// Номер порта для доставки SmtpPort
        /// </summary>
        public static Int32 SmtpPort { get; }

        /// <summary>
        /// Имя хоста доставки для email
        /// </summary>
        public static String SmtpHost { get; }

        /// <summary>
        /// логин к почтовому аккаунту с которого будет производиться рассылка
        /// </summary>
        public static String SmtpLogin { get; }

        /// <summary>
        /// Пароль к почтовому аккаунту с которого будет производиться рассылка
        /// </summary>
        public static String SmtpPassword { get; }
        /// <summary>
        /// Использовать ядро по умолчанию?
        /// </summary>
        public static Boolean DefaultCore { get; }
        /// <summary>
        /// Загружать плагины?
        /// </summary>
        public static Boolean LoadPlugin { get; }
    }

    class InternalAppConfig
    {
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
    }
}
