using System;

namespace WpfExecutor.Model
{
   public interface IPropModel
    {
        /// <summary>
        /// Значение для сравнения
        /// </summary>
        Object Value { get; set; }
        /// <summary>
        /// Тип значения
        /// </summary>
        String TypeName { get; set; }

        Boolean IsRequired { get; set; }
}
}
