using System;
using System.Globalization;

namespace WpfConverters.Converters.Format
{
    public class FormatNumberConverter : BaseHybridConverterExtension
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value is IConvertible == false)
                return null;
            decimal number;
            try
            {
                number = System.Convert.ToDecimal(value, CultureInfo.CurrentCulture);
            }
            catch (Exception)
            {
                return null;
            }
            return Format(number, parameter as string);
        }

        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 0)
                return null;

            var value = values[0];

            string format = null;
            byte? scale = null;
            bool separate = false;

            if (values.Length > 1)
            {
                scale = values[1] as byte?;
                if (scale == null)
                {
                    format = values[1] as string;
                }
            }

            if (values.Length > 2)
            {
                separate = values[2] as bool? ?? false;
            }


            if (scale != null)
            {
                if (scale == 255)
                {
                    scale = 2;
                }
                if (scale >= 0)
                {
                    if (separate)
                    {
                        format = $"#,0.{new string('0', scale.Value)}";
                    }
                    else
                    {
                        format = $"#0.{new string('0', scale.Value)}";
                    }
                }
            }

            return Convert(value, targetType, format, culture);
        }

        public static string Format(decimal value, string format = null)
        {
            var nfi = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
            nfi.NumberGroupSeparator = " ";
            nfi.NumberDecimalSeparator = ".";

            if (string.IsNullOrEmpty(format))
            {
                var s = value.ToString(nfi);
                return s;
            }
            else
            {
                var s = value.ToString(format, nfi);
                return s;
            }
        }
    }
}
