using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WpfExecutor.Model;

namespace WpfExecutor.Control
{
    /// <summary>
    /// Логика взаимодействия для TextBoxControl.xaml
    /// </summary>
    public partial class BooleanControl : CheckBox, ICustomControl
    {
        public BooleanControl()
        {
            InitializeComponent();
        }

        [UsedImplicitly]
        public static FrameworkElementFactory GetFrameworkElementFactory()
        {
            var factory = new FrameworkElementFactory(typeof(BooleanControl));
            factory.SetBinding(IsCheckedProperty, new Binding(nameof(PropModel.Value))
            {
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });
            return factory;
        }
    }
}
