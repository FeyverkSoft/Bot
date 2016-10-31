using System;
using System.Globalization;

namespace WpfConverters.Converters.Common
{
    /// <summary>
    /// Выбирает из кортежа нужный item
    /// </summary>
    public class TupleConverter : BaseValueConverterExtension
    {
        public override Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            return value;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType().Name.Contains("Tuple") && parameter is String)
            {
                return value.GetType().GetProperty((String)parameter).GetValue(value);
            }
            return value;
        }
    }
}
