using System;
using System.Diagnostics;
using System.Windows;
using Core.Core;
using WpfExecutor.Extensions.Localization;

namespace WpfExecutor.Model.About
{
    /// <summary>
    /// Модель окна с информацией о программе
    /// </summary>
    public class AboutViewModel : BaseViewModel
    {
        private static readonly FileVersionInfo Info = FileVersionInfo.GetVersionInfo(Application.ResourceAssembly.Location);
        /// <summary>
        /// Заголовок продукта
        /// </summary>
        public String Title => Info.ProductName;
        /// <summary>
        /// Описание продукта
        /// </summary>
        public String Description => Info.Comments;
        /// <summary>
        /// Версия GUI
        /// </summary>
        public String Version => Info.ProductVersion;
        /// <summary>
        /// Авторские права
        /// </summary>
        public String Copyright => Info.LegalCopyright;

        /// <summary>
        /// Версия ядра
        /// </summary>
        public String Core => AppContext.Get<IExecutiveCore>().Version.ToString();

        /// <summary>
        /// Информация о дополнительных пакетах используемых в ПО
        /// </summary>
        public String AboutWindowPackages => LocalizationManager.GetString("AboutWindow_packages");
    }
}
