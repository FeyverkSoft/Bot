using System;
using System.Globalization;
using System.Linq;

namespace WpfConverters.Converters.Common
{
    /// <summary>
    /// конвертер логическое или
    /// </summary>
    public class LogicalOrConverter : BaseMultiValueConverterExtension
    {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var booleans = values.Select(x => x as bool? ?? false);
            return booleans.FirstOrDefault(x => x);
        }
    }
}
