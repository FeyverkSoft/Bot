using System.Globalization;
using System.Windows;

namespace WpfConverters.Controls.Impl
{
   
    public class Int32UpDown : NumericUpDownGeneric<int>
    {
        static Int32UpDown()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Int32UpDown), new FrameworkPropertyMetadata(typeof(Int32UpDown)));
        }

        public Int32UpDown()
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
            int value;
            return int.TryParse(text, NumberStyles.Number, CultureInfo.CurrentCulture, out value);
        }

        protected override void UpdateValue(string text)
        {
            BeginUpdateValue();
            if (string.IsNullOrEmpty(text))
            {
                Value = null;
                return;
            }
            var value = int.Parse(text, NumberStyles.Number, CultureInfo.CurrentCulture);
            Value = value;
            EndUpdateValue();
        }
    }
}
