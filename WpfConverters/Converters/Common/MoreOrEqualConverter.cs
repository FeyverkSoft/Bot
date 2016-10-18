using System;
using System.Globalization;

namespace WpfConverters.Converters.Common
{
    public class MoreOrEqualConverter : BaseHybridConverterExtension
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var number = System.Convert.ToDouble(value);
                var param = System.Convert.ToDouble(parameter);
                return number >= param;
            }
            catch
            {
                return null;
            }
        }

        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 0)
                return null;

            var value1 = values[0];
            var value2 = values.Length > 1 ? values[1] : parameter;

            try
            {
                var number = System.Convert.ToDouble(value1);
                var param = System.Convert.ToDouble(value2);
                return number >= param;
            }
            catch
            {
                return null;
            }
        }
    }
}
