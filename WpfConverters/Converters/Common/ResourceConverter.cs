using System;
using System.Globalization;
using System.Windows;

namespace WpfConverters.Converters.Common
{
    public class ResourceConverter : BaseValueConverterExtension
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var key = value as string;
            if (string.IsNullOrEmpty(key) && string.IsNullOrWhiteSpace(key))
                return null;
            var format = parameter as string ?? "{0}";
            return Application.Current.TryFindResource(string.Format(format, key));
        }
    }
}
