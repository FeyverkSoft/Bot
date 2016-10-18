using System;
using System.Globalization;

namespace WpfConverters.Converters.Common
{
    /// <summary>
    /// Инвертирует переданное значение типа <see cref="bool"/>
    /// </summary>
    public class InverseBooleanConverter : BaseValueConverterExtension
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(value as bool?);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(value as bool?);
        }
    }
}
