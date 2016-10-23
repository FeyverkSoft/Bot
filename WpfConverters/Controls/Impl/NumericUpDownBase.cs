using System.Globalization;
using System.Windows;
using WpfConverters.Extensions.Commands;

namespace WpfConverters.Controls.Impl
{
    public abstract class NumericUpDownBase : BaseControl
    {
        static NumericUpDownBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NumericUpDownBase), new FrameworkPropertyMetadata(typeof(NumericUpDownBase)));
        }

        public DelegateCommand IncrementCommand { get; set; }
        public DelegateCommand DecrementCommand { get; set; }

        protected readonly string NegativeSign = CultureInfo.CurrentCulture.NumberFormat.NegativeSign;
        protected readonly string DecimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

        protected bool IsUpdatingValue { get; private set; }

        protected void BeginUpdateValue()
        {
            IsUpdatingValue = true;
        }

        protected void EndUpdateValue()
        {
            IsUpdatingValue = false;
        }

        protected virtual void OnIncrement()
        {
        }

        protected virtual void OnDecrement()
        {
        }

        protected virtual bool ValidateText(string text)
        {
            return true;
        }

        protected virtual void UpdateValue(string text)
        {
        }


        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            nameof(Text), typeof(string), typeof(NumericUpDownBase), new FrameworkPropertyMetadata(default(string), null, CoerceTextCallback));

        protected string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        private static object CoerceTextCallback(DependencyObject d, object baseValue)
        {
            var element = (NumericUpDownBase)d;
            return element.OnCoerceText(baseValue as string);
        }

        protected virtual object OnCoerceText(string baseValue)
        {
            return baseValue;
        }


        public static readonly DependencyProperty IsReadOnlyProperty = DependencyProperty.Register(
            nameof(IsReadOnly), typeof (bool), typeof (NumericUpDownBase), new PropertyMetadata(false));

        public bool IsReadOnly
        {
            get { return (bool) GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }
    }
}
