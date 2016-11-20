using System;
using WpfExecutor.Extensions;

namespace WpfExecutor.Model
{
    public class PropModel : BaseViewModel, IPropModel
    {
        private Object _value;

        /// <summary>
        /// Наиминование свойства
        /// </summary>
        public String PropName { get; set; }

        /// <summary>
        /// Отображаемое название параметра
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// Значение параметра
        /// </summary>
        public Object Value
        {
            get { return _value; }
            set
            {
                _value = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Это свойство является обязательным?
        /// </summary>
        public Boolean IsRequired { get; set; } = false;

        /// <summary>
        /// наименование типа данных
        /// </summary>
        public String TypeName { get; set; }
    }
}
