using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Core;
using WpfExecutor.Model;

namespace WpfExecutor.Control
{
    /// <summary>
    /// Логика взаимодействия для PluginNameControl.xaml
    /// </summary>
    public partial class PluginNameControl : ComboBox, ICustomControl
    {
        public PluginNameControl()
        {
            InitializeComponent();
        }

        [UsedImplicitly]
        public static FrameworkElementFactory GetFrameworkElementFactory()
        {
            var list = Assemblys.PluginsList.Distinct().Select(x => x.Name);
            var factory = new FrameworkElementFactory(typeof(PluginNameControl));
            factory.SetValue(ItemsSourceProperty, list);
            factory.SetBinding(SelectedItemProperty, new Binding(nameof(IPropModel.Value))
            {
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
            });
            return factory;
        }
    }
}
