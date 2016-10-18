using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace WpfConverters.Converters.Common
{
    /// <summary>
    /// Абстрактный класс для всех аррифметических конвертеров
    /// </summary>
    public abstract class ArithmeticConverter : BaseMultiValueConverterExtension
    {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Any(x => !(x is IConvertible)))
                return null;

            var numbers = values.Select(System.Convert.ToDouble).ToList();
            if (numbers.Any() != true)
                return null;

            var result = Calculate(numbers);

            if (result == null)
                return null;

            var typeCode = Type.GetTypeCode(targetType);
            switch (typeCode)
            {
                case TypeCode.Int16:
                    return (Int16)result;
                case TypeCode.Int32:
                    return (Int32)result;
                case TypeCode.Int64:
                    return (Int64)result;
                case TypeCode.Decimal:
                    return (Decimal)result;
                case TypeCode.Double:
                    return (Double)result;
            }
            return result;
        }

        protected abstract Double? Calculate(List<Double> numbers);
    }
}
