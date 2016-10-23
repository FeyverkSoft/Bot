using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.ConfigEntity;
using WpfExecutor.Model.About;
using WpfExecutor.Model.Settings;
using WpfExecutor.Views.About;
using WpfExecutor.Views.Settings;

namespace WpfExecutor.Factories
{
    internal static class WindowFactory
    {
        /// <summary>
        /// Создаёт окно о программе
        /// </summary>
        /// <returns></returns>
        public static AboutWindow CreateAboutWindow()
        {
            var viewModel = new AboutViewModel();
            var window = new AboutWindow(viewModel);
            return window;
        }
        /// <summary>
        /// Окно настроек ядра
        /// </summary>
        /// <returns></returns>
        public static CoreSettingWindow CreateCoreSettingsWindow()
        {
            var viewModel = new CoreSettingViewModel();
            var window = new CoreSettingWindow(viewModel);
            return window;
        }
    }
}
