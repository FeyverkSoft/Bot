using System;
using System.Globalization;

namespace WpfConverters.Converters.Format
{
    public class InlineStringConverter : BaseValueConverterExtension
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var text = value as string;
            return text?.Replace("\r\n", " ").Replace("\r", " ").Replace("\n", " ");
        }
    }
}
