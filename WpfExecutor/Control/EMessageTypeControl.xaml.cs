using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Core.Core;
using Core.Message;
using WpfExecutor.Converter;
using WpfExecutor.Helpers;
using WpfExecutor.Model;

namespace WpfExecutor.Control
{
    /// <summary>
    /// Логика взаимодействия для EMessageTypeControl.xaml
    /// </summary>
    public partial class EMessageTypeControl : ComboBox, ICustomControl
    {
        public EMessageTypeControl()
        {
            InitializeComponent();
        }

        [UsedImplicitly]
        public static FrameworkElementFactory GetFrameworkElementFactory()
        {
            var list = typeof(EMessageType).GeEnumTuple();
            var factory = new FrameworkElementFactory(typeof(EMessageTypeControl));
            factory.SetValue(ItemsSourceProperty, list);
            factory.SetBinding(SelectedItemProperty, new Binding(nameof(PropModel.Value))
            {
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                Converter = new TupleConverter(),
                ConverterParameter = list
            });
            return factory;
        }
    }
}
