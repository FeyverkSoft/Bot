using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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
