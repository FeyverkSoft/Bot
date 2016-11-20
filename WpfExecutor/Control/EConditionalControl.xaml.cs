using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Core.ConfigEntity.ActionObjects;
using WpfExecutor.Converter;
using WpfExecutor.Helpers;
using WpfExecutor.Model.ConditionalEditor;

namespace WpfExecutor.Control
{
    /// <summary>
    /// Логика взаимодействия для EConditionalControll.xaml
    /// </summary>
    /// 
    /// Это костыль!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    /// В падлу было искать нормальное решение всего из-за одого контролла
    /// Если таких будет 2, буду думать
    /// 
    public partial class EConditionalControl : ComboBox, ICustomControl
    {
        public EConditionalControl()
        {
            InitializeComponent();
        }

        [UsedImplicitly]
        public static FrameworkElementFactory GetFrameworkElementFactory()
        {
            var list = typeof(EConditional).GeEnumTuple();
            var factory = new FrameworkElementFactory(typeof(EConditionalControl));
            factory.SetValue(ItemsSourceProperty, list);
            factory.SetBinding(SelectedItemProperty, new Binding(nameof(ConditionalParamModel.Conditional))
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
