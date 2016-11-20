using System;
using System.Collections.Generic;

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
        public EConditional Conditional { get; set; }
    }

    public class ConditionalsParam
    {
        public List<ConditionalParam> Params { get; set; }
        public String Type { get; set; }
    }
}