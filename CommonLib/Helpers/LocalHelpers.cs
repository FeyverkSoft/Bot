using System;
using System.ComponentModel;
using System.Reflection;
using CommonLib.Attributes;

namespace CommonLib.Helpers
{
    public static class LocalHelpers
    {
        public static String GetLocalName(this Type t)
        {
          return (t?.GetCustomAttribute<LocDescriptionAttribute>() ?? t?.GetCustomAttribute<DescriptionAttribute>())?
                .Description ?? t?.Name;
        }

        public static String GetLocalName(this FieldInfo t)
        {
            return (t?.GetCustomAttribute<LocDescriptionAttribute>() ?? t?.GetCustomAttribute<DescriptionAttribute>())?
                  .Description ?? t?.Name;
        }

        public static String GetLocalName(this PropertyInfo t)
        {
            return (t?.GetCustomAttribute<LocDescriptionAttribute>() ?? t?.GetCustomAttribute<DescriptionAttribute>())?
                  .Description ?? t?.Name;
        }

        public static String GetLocalName(this MemberInfo t)
        {
            return (t?.GetCustomAttribute<LocDescriptionAttribute>() ?? t?.GetCustomAttribute<DescriptionAttribute>())?
                  .Description;
        }
    }
}
