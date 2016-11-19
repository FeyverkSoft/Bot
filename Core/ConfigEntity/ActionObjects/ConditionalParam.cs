using System;

namespace Core.ConfigEntity.ActionObjects
{
    public class ConditionalParam
    {
        /// <summary>
        /// Название поля
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Значение для сравнения
        /// </summary>
        public Object ConditionalValue { get; set; }
        /// <summary>
        /// Условный оператор, ==, !=, итд
        /// </summary>
        public Conditional Conditional { get; set; }
    }
}