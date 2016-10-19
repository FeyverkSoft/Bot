using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfExecutor.Extensions.Localization;
using WpfExecutor.Model;
using WpfExecutor.Properties;

namespace WpfExecutor
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        static App()
        {
            ToolTipService.ShowOnDisabledProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata(true));
            ToolTipService.ShowOnDisabledProperty.OverrideMetadata(typeof(FrameworkContentElement), new FrameworkPropertyMetadata(true));
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            LocalizationManager.Instance.LocalizationProvider = AppContext.Get<ILocalizationProvider>();
            LocalizationManager.Instance.CultureChanged += OnCultureChanged;

            if (!LocalizationManager.Instance.Cultures.Any(x => x.Equals(new CultureInfo(Settings.Default.Culture))))
                Settings.Default.Culture = "en-US";

            LocalizationManager.Instance.CurrentCulture = CultureInfo.GetCultureInfo(Settings.Default.Culture);

            var window = new MainWindow(new MainWindowModel());
            window.Show();
        }


        private void OnCultureChanged(object sender, EventArgs eventArgs)
        {
            Settings.Default.Culture = LocalizationManager.Instance.CurrentCulture.Name;
        }
    }
}
