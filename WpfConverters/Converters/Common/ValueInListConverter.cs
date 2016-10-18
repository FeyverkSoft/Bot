using System;
using System.Collections;
using System.Globalization;
using System.Linq;

namespace WpfConverters.Converters.Common
{
    public class ValueInListConverter : BaseMultiValueConverterExtension
    {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 2)
                return false;
            var first = values[0];
            var list = values.Skip(1).ToList();
            var result = list.Contains(first);
            if (result)
                return true;
            if (list.Count == 1)
            {
                var items = list.First() as IEnumerable;
                if (items != null && !(items is string))
                    return items.Cast<object>().Contains(first);
            }
            return false;
        }
    }
}
