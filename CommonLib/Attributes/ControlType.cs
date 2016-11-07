using System;

namespace CommonLib.Attributes
{
    /// <summary>
    /// аттрибут указывает тип контролла который необходимо использовать для работы с объектом класса
    /// </summary>
    public class ControlType : Attribute
    {
        public String Type { get; set; }

        public ControlType(String type)
        {
            Type = type;
        }
    }
}
