using System;
using Core.ConfigEntity.ActionObjects;

namespace WpfExecutor.Model.ConditionalEditor
{
   public class ConditionalParamModel
    {
        /// <summary>
        /// Название поля
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Значение для сравнения
        /// </summary>
        public Object Value { get; set; }
        /// <summary>
        /// Условный оператор, ==, !=, итд
        /// </summary>
        public Conditional Conditional { get; set; }
        /// <summary>
        /// Тип значения
        /// </summary>
        public Type ValueType { get; set; }
    }
}
