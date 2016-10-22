using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Core.Attributes;
using Core.ConfigEntity;
using WpfConverters.Converters;
using WpfExecutor.Extensions.Provider;

namespace WpfExecutor.Converter
{
    public class IfActionConverter : BaseValueConverterExtension
    {
        public override Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            var prop = value.GetType().GetProperties().Where(x => x.PropertyType.GetInterfaces().Any(y => y == typeof(ICollection)));
            var result = prop.Select(x => new IfList((x.GetCustomAttribute(typeof(LocDescriptionAttribute)) as LocDescriptionAttribute)?.Description ?? x.Name,
                (ListBotAction)x.GetValue(value)));
            return result;
        }
    }

    public class IfList
    {
        public IfList(String listName, ListBotAction list)
        {
            ListName = listName;
            List = list;
        }
        public String ListName { get; set; }
        public ListBotAction List { get; set; }
    }
}
