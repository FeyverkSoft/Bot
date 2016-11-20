using System;
using Core.ConfigEntity.ActionObjects;
using WpfExecutor.Extensions;

namespace WpfExecutor.Model.ConditionalEditor
{
   public class ConditionalParamModel:BaseViewModel, IPropModel
    {
        private EConditional _conditional;

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
        public EConditional Conditional
        {
            get { return _conditional; }
            set
            {
                _conditional = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Тип значения
        /// </summary>
        public String TypeName { get; set; }

        public Boolean IsRequired { get; set; }
    }
}
