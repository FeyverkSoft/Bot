using System;
using System.Collections;
using System.Globalization;
using System.Linq;

namespace WpfConverters.Converters.Common
{
    /// <summary>
    /// Возвращает первое значение привязки
    /// </summary>
    public class FirstItemConverter : BaseHybridConverterExtension
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var items = value as IEnumerable;
            return items?.Cast<object>().FirstOrDefault();
        }

        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 1)
            {
                var list = values[0] as IEnumerable;
                if (list != null && list is string == false)
                    return list.Cast<object>().FirstOrDefault();
            }
            return values.FirstOrDefault();
        }
    }
}
