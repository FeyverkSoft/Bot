using System;
using System.Diagnostics;
using System.Globalization;

namespace WpfConverters.Converters.Common
{
    public class ValueConverter : BaseHybridConverterExtension
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                if (value is DateTime)
                    Debug.WriteLine($"{Environment.NewLine}---ValueConverter.Convert---{Environment.NewLine}value: {((DateTime?)value).Value}\ttargetType: {targetType}");
                else
                {
                    Debug.WriteLine($"{Environment.NewLine}---ValueConverter.Convert---{Environment.NewLine}value: {value}\ttargetType: {targetType}");
                }
            }
            else
            {
                Debug.WriteLine($"{Environment.NewLine}---ValueConverter.Convert---{Environment.NewLine}value: null\ttargetType: {targetType}");
            }
            return value;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                if (value is DateTime)
                    Debug.WriteLine($"{Environment.NewLine}---ValueConverter.ConvertBack---{Environment.NewLine}value: {((DateTime?)value).Value.ToString(CultureInfo.InvariantCulture)}\ttargetType: {targetType}");
                else
                {
                    Debug.WriteLine($"{Environment.NewLine}---ValueConverter.ConvertBack---{Environment.NewLine}value: {value}\ttargetType: {targetType}");
                }
            }
            else
            {
                Debug.WriteLine($"{Environment.NewLine}---ValueConverter.ConvertBack---{Environment.NewLine}value: null\ttargetType: {targetType}");
            }
            return value;
        }

        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        public override object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
