using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Core.Core;
using WpfConverters.Converters.Common;
using WpfExecutor.Helpers;
using WpfExecutor.Model;

namespace WpfExecutor.Control
{
    /// <summary>
    /// Логика взаимодействия для KeyCodeControl.xaml
    /// </summary>
    public partial class KeyCodeControl : ComboBox, ICustomControl
    {
        public KeyCodeControl()
        {
            InitializeComponent();
        }

        [UsedImplicitly]
        public static FrameworkElementFactory GetFrameworkElementFactory()
        {
            var list = typeof(KeyCode).GeEnumTuple();
            var factory = new FrameworkElementFactory(typeof(KeyCodeControl));
            factory.SetValue(ItemsSourceProperty, list);
            factory.SetBinding(SelectedItemProperty, new Binding(nameof(PropModel.Value))
            {
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                Converter = new TupleConverter(list),
                ConverterParameter = nameof(Tuple<String, Object>.Item2)
            });
            return factory;
        }
    }
}
