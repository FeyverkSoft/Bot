using System;
using System.Globalization;

namespace WpfConverters.Converters.DataType
{
    public class DecimalConverter : BaseValueConverterExtension
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IConvertible)
            {
                try
                {
                    var dec = System.Convert.ToDecimal(value);
                    return dec;
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
