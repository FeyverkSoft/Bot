using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.ConfigEntity.ActionObjects;

namespace WpfExecutor.Model.Add
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
