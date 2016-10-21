using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WpfConverters.Converters;

namespace WpfExecutor.Converter
{
    public class LocDescriptionConverter : BaseValueConverterExtension
    {
        private static readonly Dictionary<String, MemberInfo> AttrInfo = new Dictionary<String, MemberInfo>();
        public override Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            var key = $"{value.GetType().FullName}_{value}";
            if(!AttrInfo.ContainsKey(key))
                AttrInfo.Add(key, value.GetType().GetMember(value.ToString()).FirstOrDefault());

            var attr = AttrInfo[key]?.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute;

            return attr != null ? attr.Description : value;
        }
    }
}
