using System;
using System.Globalization;

namespace WpfConverters.Converters.Common
{
    /// <summary>
    /// Объединяет строки в одну строку
    /// </summary>
    public class ConcatStringsConverter : BaseMultiValueConverterExtension
    {
        public override object Convert(Object[] values, Type targetType, Object parameter, CultureInfo culture)
        {
            return String.Join("", values);
        }
    }
}
