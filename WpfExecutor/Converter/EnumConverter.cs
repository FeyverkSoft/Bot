using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using CommonLib.Helpers;
using WpfConverters.Converters;

namespace WpfExecutor.Converter
{
    public class EnumConverter : BaseValueConverterExtension
    {
        public override Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            var t = (value as Type);
            if (t != null)
            {
                return t.GetMembers().Where(x => x.MemberType == MemberTypes.Field)
                    .Cast<FieldInfo>().Where(x=>!x.IsSpecialName)
                    .Select(memberInfo => new Tuple<String, Object>(
                        memberInfo.GetLocalName(),
                        memberInfo.GetValue(memberInfo)
                    )).ToList();
            }
            return value;
        }
    }
}
