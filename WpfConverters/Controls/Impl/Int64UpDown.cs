using System;
using System.Globalization;
using System.Windows;

namespace WpfConverters.Controls.Impl
{
   
    public class Int64UpDown : NumericUpDownGeneric<Int64>
    {
        static Int64UpDown()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Int64UpDown), new FrameworkPropertyMetadata(typeof(Int64UpDown)));
        }

        public Int64UpDown()
        {
            DecimalPlaces = 0;
        }

        protected override object OnCoerceDecimalPlaces(int baseValue)
        {
            return 0;
        }

        protected override void OnIncrement()
        {
            var value = Value ?? 0;
            value += Step;
            Value = value;
        }

        protected override void OnDecrement()
        {
            var value = Value ?? 0;
            value -= Step;
            Value = value;
        }

        protected override bool ValidateText(string text)
        {
            if (string.IsNullOrEmpty(text))
                return true;
            long value;
            return long.TryParse(text, NumberStyles.Number, CultureInfo.CurrentCulture, out value);
        }

        protected override void UpdateValue(string text)
        {
            BeginUpdateValue();
            if (string.IsNullOrEmpty(text))
            {
                Value = null;
                return;
            }
            var value = long.Parse(text, NumberStyles.Number, CultureInfo.CurrentCulture);
            Value = value;
            EndUpdateValue();
        }
    }
}
