using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Core.Core;
using WpfConverters.Converters.Common;
using WpfExecutor.Converter;
using WpfExecutor.Helpers;
using WpfExecutor.Model;

namespace WpfExecutor.Control
{
    /// <summary>
    /// Логика взаимодействия для ESearchParamControl.xaml
    /// </summary>
    public partial class EStackActionControl : ComboBox, ICustomControl
    {
        public EStackActionControl()
        {
            InitializeComponent();
        }

        [UsedImplicitly]
        public static FrameworkElementFactory GetFrameworkElementFactory()
        {
            var list = typeof(EStackAction).GeEnumTuple();
            var factory = new FrameworkElementFactory(typeof(EStackActionControl));
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
