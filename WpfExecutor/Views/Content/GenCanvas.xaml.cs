using System.Windows.Controls;
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
