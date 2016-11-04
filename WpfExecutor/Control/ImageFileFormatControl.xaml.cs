using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Core.Core;
using WpfExecutor.Model;
using WpfExecutor.Converter;
using WpfExecutor.Helpers;

namespace WpfExecutor.Control
{
    /// <summary>
    /// Логика взаимодействия для ImageFileFormatControl.xaml
    /// </summary>
    public partial class ImageFileFormatControl : ComboBox, ICustomControl
    {
        public ImageFileFormatControl()
        {
            InitializeComponent();
            var list = typeof(ImageFileFormat).GeEnumTuple();
            ItemsSource = list;
        }

        [UsedImplicitly]
        public static FrameworkElementFactory GetFrameworkElementFactory()
        {
            var list = typeof(ImageFileFormat).GeEnumTuple();
            var factory = new FrameworkElementFactory(typeof(ImageFileFormatControl));
            factory.SetValue(ItemsSourceProperty, list);
            factory.SetBinding(SelectedItemProperty, new Binding(nameof(PropModel.Value))
            {
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                Converter = new TupleConverter(),
                ConverterParameter = list,
            });
            return factory;
        }
    }
}
