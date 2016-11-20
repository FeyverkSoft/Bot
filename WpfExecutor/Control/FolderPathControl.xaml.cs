using System.Windows;
using System.Windows.Data;
using WpfExecutor.Control.ImplControl;
using WpfExecutor.Extensions;
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
            factory.SetBinding(FolderPathProperty, new Binding(nameof(IPropModel.Value))
            {
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });
            return factory;
        }
    }
}
