using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace WpfConverters.Converters.Common
{
    /// <summary>
    /// Выбирает из кортежа нужный item
    /// </summary>
    public class TupleConverter : BaseValueConverterExtension
    {
        private readonly List<Tuple<String, Object>> _list;
        public TupleConverter(List<Tuple<String, Object>> t = null)
        {
            _list = t;
        }
        public override Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
                foreach (var tuple in _list)
                {
                    if (tuple.Item2.ToString() == value.ToString())
                        return tuple;
                }
            return value;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType().Name.Contains("Tuple") && parameter is String)
            {
                return value.GetType().GetProperty((String)parameter).GetValue(value);
            }
            return value;
        }
    }
}
