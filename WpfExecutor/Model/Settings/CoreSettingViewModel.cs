using System;

namespace WpfExecutor.Model.Settings
{
    public class CoreSettingViewModel:BaseViewModel
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
