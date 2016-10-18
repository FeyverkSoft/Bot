using System;
using System.Globalization;
using System.Linq;

namespace WpfConverters.Converters.Common
{
    /// <summary>
    /// конвертер логического И
    /// </summary>
    public class LogicalAndConverter : BaseMultiValueConverterExtension
    {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var booleans = values.Select(x => x as bool? ?? false);
            return booleans.All(x => x);
        }
    }
}
