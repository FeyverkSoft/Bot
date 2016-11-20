using System.Windows;
using System.Windows.Data;
using WpfExecutor.Control.ImplControl;
using WpfExecutor.Model;

namespace WpfExecutor.Control
{
    /// <summary>
    /// Логика взаимодействия для SaveFileParamControl.xaml
    /// </summary>
    public partial class SaveFileParamControl : SaveFileParamUserControl, ICustomControl
    {
        public SaveFileParamControl()
        {
            InitializeComponent();
        }

        [UsedImplicitly]
        public static FrameworkElementFactory GetFrameworkElementFactory()
        {
            var factory = new FrameworkElementFactory(typeof(SaveFileParamControl));
            factory.SetBinding(SaveFileParamProperty, new Binding(nameof(IPropModel.Value))
            {
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });
            
            return factory;
        }
    }
}
