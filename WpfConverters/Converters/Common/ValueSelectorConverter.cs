using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WpfConverters.Converters.Common
{
    public class ValueSelectorConverter : BaseMultiValueConverterExtension
    {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 1)
                return null;

            var value = values[0];
            if (values.Length < 2)
                return value;

            var path = values[1] as string;
            if (string.IsNullOrEmpty(path))
                return value;

            var selector = new ValueSelector();
            BindingOperations.SetBinding(selector, ValueSelector.ValueProperty, new Binding(path) { Source = value });
            return selector.Value;
        }

        private class ValueSelector : DependencyObject
        {
            public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
                nameof(Value), typeof(object), typeof(ValueSelector), new PropertyMetadata(default(object)));

            public object Value
            {
                get { return GetValue(ValueProperty); }
                set { SetValue(ValueProperty, value); }
            }
        }
    }
}
