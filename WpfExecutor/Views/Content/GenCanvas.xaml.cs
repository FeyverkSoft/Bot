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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfExecutor.Model.Content;

namespace WpfExecutor.Views.Content
{
    /// <summary>
    /// Логика взаимодействия для GenCanvas.xaml
    /// </summary>
    public partial class GenCanvas : UserControl
    {
        public GenCanvas()
        {
            InitializeComponent();
            DataContext = new GenCanvasModel();
        }
    }
}
