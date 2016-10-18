using System;
using System.Globalization;

namespace WpfConverters.Converters.Common
{
    public class PropertyConverter : BaseValueConverterExtension
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            var propertyName = parameter?.ToString();
            if (string.IsNullOrEmpty(propertyName))
                return null;

            var prop = value.GetType().GetProperty(propertyName);
            if (prop.CanRead)
                return prop.GetValue(value);

            return null;
        }
    }
}
