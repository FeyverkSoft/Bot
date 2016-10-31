using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Core.Attributes;
using Core.ConfigEntity;
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
                return t.GetMembers().Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(LocDescriptionAttribute)
                || y.AttributeType == typeof(DescriptionAttribute)) && x.MemberType == MemberTypes.Field)
                    .Cast<FieldInfo>()
                    .Select(memberInfo => new Tuple<String, Object>(
                        memberInfo.GetCustomAttribute<LocDescriptionAttribute>()?.Description ?? memberInfo.GetCustomAttribute<DescriptionAttribute>()?.Description,
                        memberInfo.GetValue(memberInfo)
                    )).ToList();
            }
            return value;
        }
    }
}
