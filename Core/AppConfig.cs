using System;
using Core.ConfigEntity;

namespace Core
{
    /// <summary>
    /// Класс с конфигурацией ядра
    /// </summary>
    internal static class AppConfig
    {
        static IConfigReader<InternalAppConfig> _appConfigReader;
        private static int _smtpPort;
        private static string _smtpHost;
        private static string _smtpLogin;
        private static string _smtpPassword;

        private static void Init()
        {
            if (_appConfigReader != null)
                return;

            _appConfigReader = new ConfigReader<InternalAppConfig>("CoreConfig.json", false);
            var temp = _appConfigReader.Load();
            SmtpPort = temp.SmtpPort;
            SmtpHost = temp.SmtpHost;
            SmtpLogin = temp.SmtpLogin;
            SmtpPassword = temp.SmtpPassword;
        }

        /// <summary>
        /// Номер порта для доставки SmtpPort
        /// </summary>
        public static Int32 SmtpPort
        {
            get
            {
                Init();
                return _smtpPort;
            }
            private set { _smtpPort = value; }
        }

        /// <summary>
        /// Имя хоста доставки для email
        /// </summary>
        public static String SmtpHost
        {
            get
            {
                Init();
                return _smtpHost;
            }
            private set { _smtpHost = value; }
        }

        /// <summary>
        /// логин к почтовому аккаунту с которого будет производиться рассылка
        /// </summary>
        public static String SmtpLogin
        {
            get
            {
                Init();
                return _smtpLogin;
            }
            private set { _smtpLogin = value; }
        }

        /// <summary>
        /// Пароль к почтовому аккаунту с которого будет производиться рассылка
        /// </summary>
        public static String SmtpPassword
        {
            get
            {
                Init();
                return _smtpPassword;
            }
            private set { _smtpPassword = value; }
        }
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
    }
}
