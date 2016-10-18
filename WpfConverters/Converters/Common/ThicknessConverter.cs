using System;
using System.Globalization;
using System.Windows;

namespace WpfConverters.Converters.Common
{
    public class ThicknessConverter : BaseMultiValueConverterExtension
    {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 1)
            {
                var d = GetDouble(values[0]) ?? 0;
                return new Thickness(d);
            }
            if (values.Length == 2)
            {
                var d1 = GetDouble(values[0]) ?? 0;
                var d2 = GetDouble(values[1]) ?? 0;
                return new Thickness(d1, d2, d1, d2);
            }
            if (values.Length == 4)
            {
                var d1 = GetDouble(values[0]) ?? 0;
                var d2 = GetDouble(values[1]) ?? 0;
                var d3 = GetDouble(values[2]) ?? 0;
                var d4 = GetDouble(values[3]) ?? 0;
                return new Thickness(d1, d2, d3, d4);
            }
            return new Thickness();
        }

        private static double? GetDouble(object value)
        {
            if (value is IConvertible)
            {
                try
                {
                    return System.Convert.ToDouble(value);
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }
    }
}
