using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Input;

namespace WpfConverters.Converters.DataType
{
    public class KeyGestureConverter : BaseValueConverterExtension
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var hotkey = value as string;

            if (hotkey == null)
                return null;

            try
            {
                var converter = new System.Windows.Input.KeyGestureConverter();
                var gesture = converter.ConvertFrom(hotkey) as KeyGesture;
                return gesture;
            }
            catch
            {
                return null;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var gesture = value as KeyGesture;

            if (gesture == null)
                return null;

            return KeyGestureToString(gesture);
        }

        public static string KeyGestureToString(KeyGesture gesture)
        {
            if (gesture == null)
                return null;

            return !string.IsNullOrEmpty(gesture.DisplayString)
                ? gesture.DisplayString
                : string.Join("+", ModifierKeysToStringArray(gesture.Modifiers).Concat(KeyToStringArray(gesture.Key)));
        }

        private static IEnumerable<string> ModifierKeysToStringArray(ModifierKeys modifiers)
        {
            if (modifiers.HasFlag(ModifierKeys.Windows))
                yield return ModifierKeys.Windows.ToString();

            if (modifiers.HasFlag(ModifierKeys.Control))
                yield return ModifierKeys.Control.ToString();

            if (modifiers.HasFlag(ModifierKeys.Shift))
                yield return ModifierKeys.Shift.ToString();

            if (modifiers.HasFlag(ModifierKeys.Alt))
                yield return ModifierKeys.Alt.ToString();
}

        private static IEnumerable<string> KeyToStringArray(Key key)
        {
            if (key != Key.None)
                yield return key.ToString();
        }
    }
}
