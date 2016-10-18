using System;
using System.Globalization;

namespace WpfConverters.Converters.Format
{
    public class FormatDateConverter : BaseValueConverterExtension
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var date = value as DateTime?;
            if (date != null)
                return date.Value.ToString(parameter as string ?? "dd-MM-yyyy HH:mm");
            var dateString = value as string;
            DateTime d;
            if (string.IsNullOrEmpty(dateString) || !DateTime.TryParse(dateString, out d))
                return value;
            return d.ToString(parameter as string ?? "dd-MM-yyyy HH:mm");
        }
    }
}
