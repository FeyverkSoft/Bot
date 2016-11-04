using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Core;
using WpfConverters.Extensions.Commands;
using WpfExecutor.Extensions.Localization;

namespace WpfExecutor.Model.Settings
{
    public class CoreSettingViewModel : BaseViewModel
    {
        private readonly AppConfig _conf = AppConfig.Instance;
        /// <summary>
        /// Комманда сохранения
        /// </summary>
        private ICommand _saveCommand;
        /// <summary>
        /// Номер порта для доставки SmtpPort
        /// </summary>
        public Int32 SmtpPort
        {
            get
            {
                return _conf.SmtpPort;
            }
            set
            {
                _conf.SmtpPort = value;
            }
        }
        /// <summary>
        /// Имя хоста доставки для email
        /// </summary>
        public String SmtpHost
        {
            get
            {
                return _conf.SmtpHost;
            }
            set
            {
                _conf.SmtpHost = value;
            }
        }
        /// <summary>
        /// логин к почтовому аккаунту с которого будет производиться рассылка
        /// </summary>
        public String SmtpLogin
        {
            get
            {
                return _conf.SmtpLogin;
            }
            set
            {
                _conf.SmtpLogin = value;
            }
        }
        /// <summary>
        /// Пароль к почтовому аккаунту с которого будет производиться рассылка
        /// </summary>
        public String SmtpPassword
        {
            get
            {
                return _conf.SmtpPassword;
            }
            set
            {
                _conf.SmtpPassword = value;
            }
        }

        /// <summary>
        /// Использовать ядро по умолчанию?
        /// </summary>
        public Boolean DefaultCore
        {
            get
            {
                return _conf.DefaultCore;
            }
            set
            {
                _conf.DefaultCore = value;
            }
        }
        /// <summary>
        /// Загружать плагины?
        /// </summary>
        public Boolean LoadPlugin
        {
            get
            {
                return _conf.LoadPlugin;
            }
            set
            {
                _conf.LoadPlugin = value;
            }
        }

        public List<String> Priority => _conf.PrioritetList;

        public ICommand SaveCommand => _saveCommand ?? (_saveCommand = new DelegateCommand(SaveCommandMethod));
        /// <summary>
        /// Сохранение конфига
        /// </summary>
        private void SaveCommandMethod()
        {
            if (MessageBox.Show(
                LocalizationManager.GetString("SaveCommandMessage"),
                LocalizationManager.GetString("SaveCommandMessageTitle"), MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    AppConfig.SaveInstance();
                }
        }
    }
}
