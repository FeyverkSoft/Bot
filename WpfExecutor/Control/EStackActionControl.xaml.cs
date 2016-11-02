using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WpfConverters.Converters.Common;
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
            var factory = new FrameworkElementFactory(typeof(EStackActionControl));
            factory.SetBinding(SelectedItemProperty, new Binding(nameof(PropModel.Value))
            {
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                Converter = new TupleConverter(),
                ConverterParameter = nameof(Tuple<String, Object>.Item2)
            });
            return factory;
        }
    }
}
