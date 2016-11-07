using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WpfExecutor.Control.ImplControl;
using WpfExecutor.Model;

namespace WpfExecutor.Control
{
    /// <summary>
    /// Логика взаимодействия для FolderPathControl.xaml
    /// </summary>
    public partial class FolderPathControl : FolderPathUserControl, ICustomControl
    {
        public FolderPathControl()
        {
            InitializeComponent();
        }

        [UsedImplicitly]
        public static FrameworkElementFactory GetFrameworkElementFactory()
        {
            var factory = new FrameworkElementFactory(typeof(FolderPathControl));
            factory.SetBinding(FolderPathProperty, new Binding(nameof(PropModel.Value))
            {
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });
            return factory;
        }
    }
}
