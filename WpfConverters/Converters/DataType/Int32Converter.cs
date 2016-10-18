using System;
using System.Globalization;

namespace WpfConverters.Converters.DataType
{
    public class Int32Converter : BaseHybridConverterExtension
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            try
            {
                return System.Convert.ToInt32(value);
            }
            catch
            {
                return null;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert(value, targetType, parameter, culture);
        }


        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 1)
                return Convert(values[0], targetType, parameter, culture);
            return null;
        }
    }
}
