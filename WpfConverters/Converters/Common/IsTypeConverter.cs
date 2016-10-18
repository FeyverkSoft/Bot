using System;
using System.Globalization;

namespace WpfConverters.Converters.Common
{
    /// <summary>
    /// проверяет соответствие типу
    /// </summary>
    public class IsTypeConverter : BaseValueConverterExtension
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return false;

            var type = parameter as Type;
            if (type == null)
                return false;

            return type.IsInstanceOfType(value);
        }
    }
}
