using System;
using System.Globalization;

namespace WpfConverters.Converters.Common
{
    public class MoreThenConverter : BaseHybridConverterExtension
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double && double.IsNaN((double)value))
                return null;

            try
            {
                var number = System.Convert.ToDecimal(value);
                var param = System.Convert.ToDecimal(parameter);
                return number > param;
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
                var number = System.Convert.ToDecimal(value1);
                var param = System.Convert.ToDecimal(value2);
                return number > param;
            }
            catch
            {
                return null;
            }
        }
    }
}
