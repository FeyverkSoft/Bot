using System;
using System.Globalization;

namespace WpfConverters.Converters.Common
{
    /// <summary>
    /// перводит всё в нижний регистер
    /// </summary>
    public class LowerCaseConverter : BaseValueConverterExtension
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return System.Convert.ToString(value ?? "").ToLower();
        }
    }
}
