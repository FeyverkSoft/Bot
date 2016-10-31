using System;
using Core.ConfigEntity;
using WpfExecutor.Model;
using WpfExecutor.Model.About;
using WpfExecutor.Model.Add;
using WpfExecutor.Model.Settings;
using WpfExecutor.Views.About;
using WpfExecutor.Views.Add;
using WpfExecutor.Views.Settings;

namespace WpfExecutor.Factories
{
    internal static class WindowFactory
    {
        /// <summary>
        /// Создаёт главное окно
        /// </summary>
        /// <returns></returns>
        public static MainWindow CreateMainWindow(String[] args)
        {
            var window = new MainWindow(new MainWindowModel(args));
            return window;
        }
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

        /// <summary>
        /// Окно добавления действия
        /// </summary>
        /// <returns></returns>
        public static AddActionWindow CreateAddActionWindow(ActionType actionType)
        {
            var viewModel = new AddActionViewModel(actionType);
            var window = new AddActionWindow(viewModel);
            return window;
        }

        /// <summary>
        /// Окно добавления действия длябота
        /// </summary>
        /// <returns></returns>
        public static AddBotActionWindow CreateAddBotActionWindow()
        {
            var viewModel = new AddBotActionModel();
            var window = new AddBotActionWindow(viewModel);
            return window;
        }
    }
}
