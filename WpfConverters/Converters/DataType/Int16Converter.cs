using System;
using System.Globalization;

namespace WpfConverters.Converters.DataType
{
    public class Int16Converter : BaseValueConverterExtension
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IConvertible)
            {
                try
                {
                    var shrt = System.Convert.ToInt16(value);
                    return shrt;
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
