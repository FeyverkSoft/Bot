using System.Windows;
using System.Windows.Data;
using WpfExecutor.Control.ImplControl;
using WpfExecutor.Model;

namespace WpfExecutor.Control
{
    /// <summary>
    /// Логика взаимодействия для SizeControl.xaml
    /// </summary>
    public partial class SizeControl : SizeUserControl, ICustomControl
    {
        public SizeControl()
        {
            InitializeComponent();
        }

        [UsedImplicitly]
        public static FrameworkElementFactory GetFrameworkElementFactory()
        {
            var factory = new FrameworkElementFactory(typeof(SizeControl));
            factory.SetBinding(SizeProperty, new Binding(nameof(IPropModel.Value))
            {
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });
            return factory;
        }
    }
}
