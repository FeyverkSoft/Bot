using System;
using System.ComponentModel;
using System.Reflection;

namespace CommonLib.Attributes
{
    /// <summary>
    /// Локализованный аттрибут описания
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class LocDescriptionAttribute : DescriptionAttribute
    {
        readonly Type _res;

        public LocDescriptionAttribute() : this(String.Empty, null)
        {
        }

        public LocDescriptionAttribute(String description, Type resType) : base(description)
        {
            _res = resType; ;
        }


        public override String Description => (String)_res?.GetProperty(DescriptionValue, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)?
                .GetValue(typeof(String), null) ?? DescriptionValue;

    }
}
