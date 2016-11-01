using System.Windows;
using System.Windows.Data;
using WpfConverters.Controls.Impl;
using WpfExecutor.Model;

namespace WpfExecutor.Control
{
    /// <summary>
    /// Логика взаимодействия для TextBoxControl.xaml
    /// </summary>
    public partial class Int32Control : Int32UpDown, ICustomControl
    {
        public Int32Control()
        {
            InitializeComponent();
        }

        [UsedImplicitly]
        public static FrameworkElementFactory GetFrameworkElementFactory()
        {
            var factory = new FrameworkElementFactory(typeof(Int32Control));
            factory.SetBinding(ValueProperty, new Binding(nameof(PropModel.Value))
            {
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });
            factory.SetBinding(IsRequiredProperty, new Binding(nameof(PropModel.IsRequired)));
            return factory;
        }
    }
}
