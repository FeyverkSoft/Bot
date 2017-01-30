using System.ComponentModel;
using System.Windows;
using ImgComparer.Model;

namespace ImgComparer
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
    }
}
