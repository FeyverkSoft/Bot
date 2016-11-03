using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Core.Attributes;

namespace WpfExecutor.Helpers
{
  public static class EnumHelper
    {
        public static List<Tuple<String, Object>> GeEnumTuple(this Type t)
        {
            return t.GetMembers().Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(LocDescriptionAttribute)
                || y.AttributeType == typeof(DescriptionAttribute)) && x.MemberType == MemberTypes.Field)
                    .Cast<FieldInfo>()
                    .Select(memberInfo => new Tuple<String, Object>(
                        memberInfo.GetCustomAttribute<LocDescriptionAttribute>()?.Description ?? memberInfo.GetCustomAttribute<DescriptionAttribute>()?.Description,
                        memberInfo.GetValue(memberInfo)
                    )).ToList();
        }
    }
}
