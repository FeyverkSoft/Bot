using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Attributes
{
    /// <summary>
    /// Локализованный аттрибут описания
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    internal class LocDescriptionAttribute : DescriptionAttribute
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
