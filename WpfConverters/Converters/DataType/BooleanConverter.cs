using System;
using System.Globalization;

namespace WpfConverters.Converters.DataType
{
    public class BooleanConverter : BaseValueConverterExtension
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return System.Convert.ToBoolean(value);
            }
            catch
            {
                // ignored
            }

            var ifNullValue = parameter as bool?;
            var s = System.Convert.ToString(value).Trim();
            if (string.IsNullOrEmpty(s))
                return ifNullValue;

            double val;
            if (double.TryParse(s, out val))
            {
                return System.Convert.ToBoolean(val);
            }

            return ifNullValue;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert(value, targetType, parameter, culture);
        }
    }
}
