using System.Windows;
using WpfExecutor.Model.Settings;

namespace WpfExecutor.Views.Settings
{
    /// <summary>
    /// Логика взаимодействия для CoreSettingWindow.xaml
    /// </summary>
    public partial class CoreSettingWindow : Window
    {
        public CoreSettingWindow(CoreSettingViewModel model)
        {
            InitializeComponent();
            DataContext = model;
        }
    }
}
