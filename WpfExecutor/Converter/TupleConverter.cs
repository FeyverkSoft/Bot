using System;
using System.Collections.Generic;
using System.Globalization;
using WpfConverters.Converters;
using WpfExecutor.Helpers;

namespace WpfExecutor.Converter
{
    /// <summary>
    /// Выбирает из кортежа нужный item
    /// </summary>
    public class TupleConverter : BaseValueConverterExtension
    {
        public override Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            List<Tuple<String, Object>> enumerable = null;
            if (parameter is Type && ((Type)parameter).IsEnum)
                enumerable = ((Type)parameter).GeEnumTuple();
            else
                if (parameter is List<Tuple<String, Object>>)
                enumerable = parameter as List<Tuple<String, Object>>;
            if (enumerable != null)
                foreach (var tuple in enumerable)
                {
                    if (tuple.Item2.ToString() == value?.ToString())
                        return tuple;
                }
            return value;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType().Name.Contains("Tuple"))
            {
                return value.GetType().GetProperty(nameof(Tuple<String, Object>.Item2)).GetValue(value);
            }
            return value;
        }
    }
}
