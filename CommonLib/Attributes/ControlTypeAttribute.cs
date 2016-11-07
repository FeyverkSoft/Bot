using System;

namespace CommonLib.Attributes
{
    /// <summary>
    /// аттрибут указывает тип контролла который необходимо использовать для работы с объектом класса
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class ControlTypeAttribute : Attribute
    {
        public String Type { get; set; }

        public ControlTypeAttribute(String type)
        {
            Type = type;
        }
    }
}
