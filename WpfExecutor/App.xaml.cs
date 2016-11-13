using System;
using System.Globalization;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Core.ConfigEntity;
using LogWrapper;
using WpfExecutor.Extensions.Localization;
using WpfExecutor.Factories;
using WpfExecutor.Model.Error;
using WpfExecutor.Properties;
using WpfExecutor.Views.Error;

namespace WpfExecutor
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static ExceptionWindow ExWindow { get; set; }
        static App()
        {
            ToolTipService.ShowOnDisabledProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata(true));
            ToolTipService.ShowOnDisabledProperty.OverrideMetadata(typeof(FrameworkContentElement), new FrameworkPropertyMetadata(true));
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ExWindow = new ExceptionWindow();
            LocalizationManager.Instance.LocalizationProvider = AppContext.Get<ILocalizationProvider>();
            LocalizationManager.Instance.CultureChanged += OnCultureChanged;

            if (!LocalizationManager.Instance.Cultures.Any(x => x.Equals(new CultureInfo(Settings.Default.Culture))))
                Settings.Default.Culture = "en-US";

            LocalizationManager.Instance.CurrentCulture = CultureInfo.GetCultureInfo(Settings.Default.Culture);

            AppDomain.CurrentDomain.FirstChanceException += CurrentDomain_FirstChanceException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Dispatcher.UnhandledException += Dispatcher_UnhandledException;

#if DEBUG
            WindowFactory.CreateConditionalEditorWindow().ShowDialog();
            //WindowFactory.CreateAddActionWindow(ActionType.GetScreenshot).ShowDialog();
            Application.Current.Shutdown();
#else
            WindowFactory.CreateMainWindow(e.Args).Show();
#endif
        }


        private void OnCultureChanged(object sender, EventArgs eventArgs)
        {
            Settings.Default.Culture = LocalizationManager.Instance.CurrentCulture.Name;
            Settings.Default.Save();
        }

        private void CurrentDomain_FirstChanceException(object sender, FirstChanceExceptionEventArgs e)
        {
            Log.WriteLine(e.Exception, LogLevel.Error);
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Log.WriteLine(e.ExceptionObject, LogLevel.Error);
            var err = e.ExceptionObject as Exception;
            Dispatcher.Invoke(() =>
            {
                ExWindow.ShowDialog(new ExceptionViewModel(err, err?.Message));
            });
        }

        private void Dispatcher_UnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            // Костыль для обработки CLIPBRD_E_CANT_OPEN, чтобы не выходило сообщение об ошибке
            if (e.Exception is System.Runtime.InteropServices.COMException &&
                e.Exception.Message.Contains("CLIPBRD_E_CANT_OPEN"))
            {
                e.Handled = true;
                return;
            }
            Log.WriteLine(e.Exception, LogLevel.Error);

            ExWindow.ShowDialog(new ExceptionViewModel(e.Exception, e.Exception?.Message));
            e.Handled = true;
        }

    }
}
