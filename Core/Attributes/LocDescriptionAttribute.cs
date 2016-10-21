using System;
using System.ComponentModel;

namespace Core.Attributes
{
    /// <summary>
    /// Локализованный аттрибут описания
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class LocDescriptionAttribute : DescriptionAttribute
    {

        public LocDescriptionAttribute() : this(String.Empty)
        {
        }

        public LocDescriptionAttribute(String description) : base(description)
        {
        }

        public override String Description => CoreText.ResourceManager.GetString(DescriptionValue) ?? DescriptionValue;

    }
}
