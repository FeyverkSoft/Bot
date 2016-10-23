using System;
using System.Globalization;
using System.Windows;

namespace WpfConverters.Controls.Impl
{


    public class DoubleUpDown : NumericUpDownGeneric<double>
    {
        static DoubleUpDown()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DoubleUpDown), new FrameworkPropertyMetadata(typeof(DoubleUpDown)));
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
            double value;
            return double.TryParse(text, NumberStyles.Number, CultureInfo.CurrentCulture, out value);
        }

        protected override void UpdateValue(string text)
        {
            BeginUpdateValue();
            if (string.IsNullOrEmpty(text))
            {
                Value = null;
                return;
            }
            var value = double.Parse(text, NumberStyles.Number, CultureInfo.CurrentCulture);
            value = value - value % (1d / Math.Pow(10, DecimalPlaces));
            value = Math.Round(value, DecimalPlaces);
            Value = value;
            EndUpdateValue();
        }
    }
}
