using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WpfExecutor.Model;

namespace WpfExecutor.Control
{
    /// <summary>
    /// Логика взаимодействия для TextBoxControl.xaml
    /// </summary>
    public partial class StringControl : TextBox, ICustomControl
    {
        public StringControl()
        {
            InitializeComponent();
        }

        [UsedImplicitly]
        public static FrameworkElementFactory GetFrameworkElementFactory()
        {
            var factory = new FrameworkElementFactory(typeof(StringControl));
            factory.SetBinding(TextProperty, new Binding(nameof(PropModel.Value))
            {
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });
            return factory;
        }
    }
}
