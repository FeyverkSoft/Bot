using System;
using System.Globalization;
using System.Windows;

namespace WpfConverters.Controls.Impl
{

    public class DecimalUpDown : NumericUpDownGeneric<decimal>
    {
        static DecimalUpDown()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DecimalUpDown), new FrameworkPropertyMetadata(typeof(DecimalUpDown)));
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
            decimal value;
            return decimal.TryParse(text, NumberStyles.Number, CultureInfo.CurrentCulture, out value);
        }

        protected override void UpdateValue(string text)
        {
            BeginUpdateValue();
            if (string.IsNullOrEmpty(text))
            {
                Value = null;
                return;
            }
            var value = decimal.Parse(text, NumberStyles.Number, CultureInfo.CurrentCulture);
            value = value - (value % (1M / new decimal(Math.Pow(10, DecimalPlaces))));
            value = Math.Round(value, DecimalPlaces);
            Value = value;
            EndUpdateValue();
        }
    }
}
