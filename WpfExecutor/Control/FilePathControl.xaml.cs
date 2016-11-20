using System.Windows;
using System.Windows.Data;
using WpfExecutor.Control.ImplControl;
using WpfExecutor.Model;

namespace WpfExecutor.Control
{
    /// <summary>
    /// Логика взаимодействия для FilePathControl.xaml
    /// </summary>
    public partial class FilePathControl : FilePathUserControl, ICustomControl
    {
        public FilePathControl()
        {
            InitializeComponent();
        }

        [UsedImplicitly]
        public static FrameworkElementFactory GetFrameworkElementFactory()
        {
            var factory = new FrameworkElementFactory(typeof(FilePathControl));
            factory.SetBinding(FilePathProperty, new Binding(nameof(IPropModel.Value))
            {
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });
            return factory;
        }
    }
}
