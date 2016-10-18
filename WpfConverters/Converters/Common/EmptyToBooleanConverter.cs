using System;
using System.Collections;
using System.Globalization;
using System.Linq;

// ReSharper disable OperatorIsCanBeUsed
namespace WpfConverters.Converters.Common
{
    /// <summary>
    /// Возвращает <c>false</c>, если переданный объект равен <c>null</c>, либо пустой. Иначе - <c>true</c>
    /// </summary>
    public class EmptyToBooleanConverter : BaseHybridConverterExtension
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null ||
                (value.GetType() == typeof(int) && (int)value == 0) ||
                (value.GetType() == typeof(long) && (long)value == 0) ||
                (value.GetType() == typeof(string) && string.IsNullOrEmpty((string)value)) ||
                (value is IEnumerable && !((IEnumerable)value).Cast<object>().Any()))
                return false;
            return true;
        }

        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values.Any(value => Convert(value, targetType, parameter, culture) as bool? ?? false);
        }
    }
}
