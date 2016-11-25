using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Core.Handlers.KeyBoard;
using WpfExecutor.Converter;
using WpfExecutor.Helpers;
using WpfExecutor.Model;

namespace WpfExecutor.Control
{
    /// <summary>
    /// Логика взаимодействия для KeyActionControl.xaml
    /// </summary>
    public partial class KeyActionControl : ComboBox, ICustomControl
    {
        public KeyActionControl()
        {
            InitializeComponent();
        }

        [UsedImplicitly]
        public static FrameworkElementFactory GetFrameworkElementFactory()
        {
            var list = typeof(KeyAction).GeEnumTuple();
            var factory = new FrameworkElementFactory(typeof(KeyActionControl));
            factory.SetValue(ItemsSourceProperty, list);
            factory.SetBinding(SelectedItemProperty, new Binding(nameof(IPropModel.Value))
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
