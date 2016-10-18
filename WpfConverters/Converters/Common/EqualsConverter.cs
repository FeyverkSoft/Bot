using System;
using System.Globalization;
using System.Linq;

namespace WpfConverters.Converters.Common
{
    /// <summary>
    /// Возвращает <c>true</c>, если все значения в последовательности равны между собой. Иначе <c>false</c>
    /// </summary>
    public class EqualsConverter : BaseHybridConverterExtension
    {
        public override Object Convert(Object[] values, Type targetType, Object parameter, CultureInfo culture)
        {
            if (values == null || !values.Any())
                return true;
            if (values.Any(x => x == null) && values.Any(x => x != null))
                return false;

            var value = parameter ?? values.First();

            if (value == null)
                return values.All(x => x == null);

            var valueType = value.GetType();
            var valueUnderlyingType = Nullable.GetUnderlyingType(valueType) ?? valueType;

            var result = values.All(x => EqualsValues(value, x, valueType, valueUnderlyingType));
            return result;
        }

        public override Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            if (value == null && parameter == null)
                return true;

            if (value == null || parameter == null)
                return false;

            return EqualsValues(value, parameter, null, null);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = value as bool?;
            if (val == true)
                return parameter;
            return null;
        }

        private static bool EqualsValues(object value1, object value2, Type valueType, Type valueUnderlyingType)
        {
            if (value1 == null && value2 == null)
                return true;

            if (value1 == null || value2 == null)
                return false;

            var eq = value1.Equals(value2);

            if (eq)
                return true;

            var value1Type = valueType ?? value1.GetType();
            var value1UnderlyingType = valueUnderlyingType ?? Nullable.GetUnderlyingType(value1Type) ?? value1Type;

            var value2Type = value2.GetType();
            var value2UnderlyingType = Nullable.GetUnderlyingType(value2Type) ?? value2Type;

            if (value2UnderlyingType == value1UnderlyingType)
                return false;

            if (value2 is IConvertible)
            {
                try
                {
                    var xValue = System.Convert.ChangeType(value2, value1UnderlyingType);
                    return value1.Equals(xValue);
                }
                catch
                {
                    // ignored
                }
            }

            if (value1UnderlyingType != value1Type)
                try
                {
                    var xValue = System.Convert.ChangeType(value2, value1Type);
                    return value1.Equals(xValue);
                }
                catch
                {
                    // ignored
                }

            return false;
        }

        public static bool EqualsValues(object value1, object value2)
        {
            return EqualsValues(value1, value2, null, null);
        }
    }
}
