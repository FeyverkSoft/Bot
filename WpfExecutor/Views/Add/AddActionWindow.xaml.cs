using System.Windows;
using WpfExecutor.Model.Add;

namespace WpfExecutor.Views.Add
{
    /// <summary>
    /// Логика взаимодействия для AddAction.xaml
    /// </summary>
    public partial class AddActionWindow : Window
    {
        public AddActionWindow(AddActionViewModel model)
        {
            InitializeComponent();
            model.Close += (sender, b) =>
             {
                 DialogResult = b;
                 Close();
             };
            DataContext = model;
        }
    }
}
