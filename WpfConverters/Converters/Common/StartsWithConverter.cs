using System;
using System.Globalization;

namespace WpfConverters.Converters.Common
{
    public class StartsWithConverter : BaseValueConverterExtension
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = value?.ToString() ?? "";
            var start = parameter?.ToString() ?? "";

            return str.StartsWith(start);
        }
    }
}
