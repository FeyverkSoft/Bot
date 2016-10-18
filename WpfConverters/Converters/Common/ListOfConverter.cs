using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace WpfConverters.Converters.Common
{
    public class ListOfConverter : BaseMultiValueConverterExtension
    {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var type = parameter as Type;
            if (type == null)
                return values;

            var result = GetValues(values, type).ToList();
            return result;
        }

        private IEnumerable<object> GetValues(IEnumerable<object> values, Type type)
        {
            foreach (var value in values.Where(x => x != null))
            {
                if (type.IsInstanceOfType(value))
                {
                    yield return value;
                }

                var list = value as IEnumerable;
                if (list != null && list is string == false)
                {
                    foreach (var item in GetValues(list.Cast<object>(), type))
                    {
                        yield return item;
                    }
                }
            }
        }
    }
}
