using System.Windows;
using WpfExecutor.Model.Add;

namespace WpfExecutor.Views.Add
{
    /// <summary>
    /// Логика взаимодействия для AddAction.xaml
    /// </summary>
    public partial class AddBotActionWindow : Window
    {
        public AddBotActionWindow(AddBotActionModel model)
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
