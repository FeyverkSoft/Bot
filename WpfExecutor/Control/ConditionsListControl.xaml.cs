using System.Windows;
using System.Windows.Data;
using WpfExecutor.Control.ImplControl;
using WpfExecutor.Extensions;
using WpfExecutor.Model;

namespace WpfExecutor.Control
{
    /// <summary>
    /// Логика взаимодействия для PointControl.xaml
    /// </summary>
    public partial class ConditionsListControl : ConditionListUserControl, ICustomControl
    {
        public ConditionsListControl()
        {
            InitializeComponent();
        }

        [UsedImplicitly]
        public static FrameworkElementFactory GetFrameworkElementFactory()
        {
            var factory = new FrameworkElementFactory(typeof(ConditionsListControl));
            factory.SetBinding(ConditionListProperty, new Binding(nameof(IPropModel.Value))
            {
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });
            return factory;
        }
    }
}
