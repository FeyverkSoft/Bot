using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Core.ConfigEntity.ActionObjects;
using Core.Core;
using Core.Message;
using WpfExecutor.Converter;
using WpfExecutor.Helpers;
using WpfExecutor.Model;

namespace WpfExecutor.Control
{
    /// <summary>
    /// Логика взаимодействия для EConditionalControll.xaml
    /// </summary>
    public partial class EConditionalControll : ComboBox, ICustomControl
    {
        public EConditionalControll()
        {
            InitializeComponent();
            ItemsSource = typeof(Conditional).GeEnumTuple();
        }

        [UsedImplicitly]
        public static FrameworkElementFactory GetFrameworkElementFactory()
        {
            var list = typeof(Conditional).GeEnumTuple();
            var factory = new FrameworkElementFactory(typeof(EConditionalControll));
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
