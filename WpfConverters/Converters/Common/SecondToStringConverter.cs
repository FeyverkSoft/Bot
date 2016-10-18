using System;
using System.Globalization;

namespace WpfConverters.Converters.Common
{
    public class SecondToStringConverter : BaseValueConverterExtension
    {
        /// <summary>
        /// Преобразовывает заданное значение. 
        /// </summary>
        /// <returns>
        /// Преобразованное значение.Если метод вернет nullдопустимое значение NULL, используется.
        /// </returns>
        /// <param name="value">Значение, произведенное источником привязки.</param><param name="targetType">Тип свойства целевого объекта привязки.</param><param name="parameter">Используемый параметр преобразователя.</param><param name="culture">Язык и региональные параметры, используемые в преобразователе.</param>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int)
            {
                var offset = (int)value;
                var absOffset = Math.Abs(offset);
                var hour = ((absOffset / 3600)).ToString();
                var minute = ((absOffset - ((absOffset / 3600) * 3600)) / 60).ToString();
                return $"{(offset >= 0 ? "" : "-")}{hour.PadLeft(2, '0').Substring(0, 2)}:{minute.PadLeft(2, '0').Substring(0, 2)}";
            }
            return value;
        }

        /// <summary>
        /// Преобразовывает заданное значение. 
        /// </summary>
        /// <returns>
        /// Преобразованное значение.Если метод вернет nullдопустимое значение NULL, используется.
        /// </returns>
        /// <param name="value">Значение, сформированное целевым объектом привязки.</param><param name="targetType">Тип, который необходимо преобразовать.</param><param name="parameter">Используемый параметр преобразователя.</param><param name="culture">Язык и региональные параметры, используемые в преобразователе.</param>
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var arr = (value as string)?.Split(':');
            if (arr?.Length == 2)
            {
                long hour, minute;
                if (Int64.TryParse(arr[0], out hour) && Int64.TryParse(arr[1], out minute))
                {
                    return hour * 3600 + minute * 60;
                }
            }
            return value;
        }
    }
}
