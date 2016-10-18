using System;
using System.Globalization;

// ReSharper disable OperatorIsCanBeUsed

namespace WpfConverters.Converters.Common
{
    /// <summary>
    /// Возвращает <c>true</c>, если переданный объект равен <c>null</c>, либо пустой. Иначе - <c>false</c>
    /// </summary>
    public class EmptyToInverseBooleanConverter : EmptyToBooleanConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = base.Convert(value, targetType, parameter, culture) as bool?;
            return !result ?? false;
        }
    }
}
