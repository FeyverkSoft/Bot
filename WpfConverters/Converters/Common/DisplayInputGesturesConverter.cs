using System;
using System.Globalization;
using System.Linq;
using System.Windows.Input;

namespace WpfConverters.Converters.Common
{
    /// <summary>
    /// Конвертер для отображения жесты устройства ввода.
    /// </summary>
    public class DisplayInputGesturesConverter : BaseValueConverterExtension
    {
        public override Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            var gestures = value as InputGestureCollection;
            if (gestures == null)
                return null;

            var displays = gestures.Cast<KeyGesture>().Select(x => x.GetDisplayStringForCulture(CultureInfo.CurrentCulture));
            var result = String.Join(", ", displays);

            if (String.IsNullOrEmpty(result) || String.IsNullOrWhiteSpace(result))
                return null;

            var format = parameter as String;

            if (String.IsNullOrEmpty(format))
                return result;

            return String.Format(format, result);
        }
    }
}
