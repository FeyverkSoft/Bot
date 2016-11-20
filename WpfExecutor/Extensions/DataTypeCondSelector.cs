using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using WpfExecutor.Control;
using WpfExecutor.Model.ConditionalEditor;

namespace WpfExecutor.Extensions
{
    /// <summary>
    /// Костыль
    /// </summary>
    public class DataTypeCondSelector : DataTemplateSelector
    {
        private static IEnumerable<Type> _dataTypeControls;

        private IEnumerable<Type> DataTypeControls
        {
            get
            {
                return _dataTypeControls ??
                       (_dataTypeControls =
                           Assembly.GetExecutingAssembly()
                               .GetTypes()
                               .Where(t => t.GetInterfaces().Contains(typeof(ICustomControl))));
            }
        }
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var param = item as ConditionalParamModel;
            if (param == null)
                return null;
            var controlTypeName = String.IsNullOrEmpty(param.Conditional.GetType().Name) ? null : param.Conditional.GetType().Name.Replace("_", "");
            var controlType =
                DataTypeControls.FirstOrDefault(
                    t =>
                        t.Name.Equals(controlTypeName, StringComparison.InvariantCultureIgnoreCase) ||
                        t.Name.Equals($"{controlTypeName}Control", StringComparison.InvariantCultureIgnoreCase)) ??
                typeof(StringControl);
            var factory =
                (FrameworkElementFactory)
                    controlType.GetMethod(nameof(StringControl.GetFrameworkElementFactory),
                        BindingFlags.Public | BindingFlags.Static)?.Invoke(null, null);
            factory?.SetValue(FrameworkElement.DataContextProperty, param);
            factory?.SetValue(FrameworkElement.MinHeightProperty, 24d);
            var template = new DataTemplate(typeof(ConditionalParamModel))
            {
                VisualTree = factory
            };
            return template;
        }

    }
}
