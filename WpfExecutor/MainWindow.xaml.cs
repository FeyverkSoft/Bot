using System.ComponentModel;
using System.Windows;
using WpfExecutor.Model;

namespace WpfExecutor
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(MainWindowModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
