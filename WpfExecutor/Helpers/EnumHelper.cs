using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CommonLib.Helpers;

namespace WpfExecutor.Helpers
{
    public static class EnumHelper
    {
        public static List<Tuple<String, Object>> GeEnumTuple(this Type t)
        {
            if (!t.IsEnum)
                throw new NotSupportedException("Is not enum!");
            return t.GetMembers().Where(x => x.MemberType == MemberTypes.Field).Cast<FieldInfo>().Where(x => !x.IsSpecialName)
                 .Select(memberInfo => new Tuple<String, Object>(
                         memberInfo.GetLocalName(),
                         memberInfo.GetValue(memberInfo)
                     )).ToList();
        }
    }
}
