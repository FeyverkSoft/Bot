using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WpfExecutor.Model;

namespace WpfExecutor.Control
{
    /// <summary>
    /// Логика взаимодействия для SizeControl.xaml
    /// </summary>
    public partial class SizeControl : UserControl, ICustomControl
    {
        public SizeControl()
        {
            InitializeComponent();
        }

        [UsedImplicitly]
        public static FrameworkElementFactory GetFrameworkElementFactory()
        {
            var factory = new FrameworkElementFactory(typeof(SizeControl));
            factory.SetBinding(DataContextProperty, new Binding(nameof(PropModel.Value))
            {
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });
            return factory;
        }
    }
}
