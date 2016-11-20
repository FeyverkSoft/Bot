using System;

namespace CommonLib.Attributes
{
    /// <summary>
    /// аттрибут указывает что потомков данного свойства необходимо игнорировать при построении визуальной модели
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class VisualCtorIgnoreChildProp : Attribute
    {
        public VisualCtorIgnoreChildProp()
        {
        }
    }
}
