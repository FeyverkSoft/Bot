using System;
using System.Globalization;
using System.Windows;
using WpfConverters.Helpers;

namespace WpfConverters.Converters.Common
{
    public class VisualChildConverter : BaseValueConverterExtension
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var element = value as DependencyObject;
            if (element == null)
                return null;
            var type = parameter as Type;
            if (type == null)
                return null;
            return VisualHelper.FindVisualChild(element, type);
        }
    }
}
