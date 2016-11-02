using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using WpfConverters.Extensions.Commands;

namespace WpfConverters.Controls.Impl
{
    [TemplatePart(Name = "PART_InputTextBox", Type = typeof(TextBox))]
    [TemplatePart(Name = "PART_IncrementButton", Type = typeof(RepeatButton))]
    [TemplatePart(Name = "PART_DecrementButton", Type = typeof(RepeatButton))]
    public abstract class NumericUpDownGeneric<T> : NumericUpDownBase where T : struct, IFormattable, IConvertible
    {
        private TextBox _inputTextBox;
        private RepeatButton _incrementButton;
        private RepeatButton _decrementButton;

        private bool IsUpdatingText { get; set; }

        private bool IsUpdatingTextBox { get; set; }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _inputTextBox = GetTemplateChild("PART_InputTextBox") as TextBox;
            _incrementButton = GetTemplateChild("PART_IncrementButton") as RepeatButton;
            _decrementButton = GetTemplateChild("PART_DecrementButton") as RepeatButton;
            ApplyEvents();
            ApplyCommands();

            UpdateText();
        }

        private void ApplyEvents()
        {
            if (_inputTextBox != null)
            {
                _inputTextBox.PreviewMouseWheel += InputTextBoxOnPreviewMouseWheel;
                _inputTextBox.PreviewTextInput += InputTextBoxOnPreviewTextInput;
                _inputTextBox.TextChanged += InputTextBoxOnTextChanged;
                DataObject.AddPastingHandler(_inputTextBox, InputTextBoxOnPasting);
            }

            GotFocus += OnGotFocus;
        }

        private void ApplyCommands()
        {
            IncrementCommand = new DelegateCommand(Increment);
            DecrementCommand = new DelegateCommand(Decrement);
            if (_inputTextBox != null)
            {
                _inputTextBox.InputBindings.Add(new KeyBinding(IncrementCommand, Key.Up, ModifierKeys.None));
                _inputTextBox.InputBindings.Add(new KeyBinding(DecrementCommand, Key.Down, ModifierKeys.None));
            }
            if (_incrementButton != null)
                _incrementButton.Command = IncrementCommand;
            if (_decrementButton != null)
                _decrementButton.Command = DecrementCommand;
        }

        private void OnGotFocus(object sender, RoutedEventArgs routedEventArgs)
        {
            _inputTextBox?.Focus();
        }

        protected override void OnClear()
        {
            Value = null;
        }

        private void Increment()
        {
            BeginUpdateValue();
            OnIncrement();
            EndUpdateValue();
            if (_inputTextBox != null)
                _inputTextBox.CaretIndex = _inputTextBox.Text.Length;
        }

        private void Decrement()
        {
            BeginUpdateValue();
            OnDecrement();
            EndUpdateValue();
            if (_inputTextBox != null)
                _inputTextBox.CaretIndex = _inputTextBox.Text.Length;
        }

        public event DependencyPropertyChangedEventHandler ValueChanged;

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            nameof(Value), typeof(T?), typeof(NumericUpDownGeneric<T>), new FrameworkPropertyMetadata(null, 
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, ValueChangedCallback, CoerceValueCallback));

        public T? Value
        {
            get { return (T?)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        private static void ValueChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = (NumericUpDownGeneric<T>)d;
            element.OnValueChanged((T?)e.OldValue, (T?)e.NewValue);
        }

        protected virtual void OnValueChanged(T? oldValue, T? newValue)
        {
            BeginUpdateValue();
            ValueChanged?.Invoke(this, new DependencyPropertyChangedEventArgs(ValueProperty, oldValue, newValue));
            UpdateText();
            EndUpdateValue();
        }

        private static object CoerceValueCallback(DependencyObject d, object baseValue)
        {
            var element = (NumericUpDownGeneric<T>)d;
            return element.OnCoerceValue((T?)baseValue);
        }

        protected virtual object OnCoerceValue(T? baseValue)
        {
            if (baseValue == null)
                return null;
            if (Comparer<T>.Default.Compare(baseValue.Value, Minimum) < 0)
                return Minimum;
            if (Comparer<T>.Default.Compare(baseValue.Value, Maximum) > 0)
                return Maximum;
            return baseValue;
        }


        #region Properties

        public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register(
            nameof(Minimum), typeof(T), typeof(NumericUpDownGeneric<T>), new PropertyMetadata(default(T), null, CoerceMinimumCallback));

        public T Minimum
        {
            get { return (T)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        private static object CoerceMinimumCallback(DependencyObject d, object baseValue)
        {
            var element = d as NumericUpDownGeneric<T>;
            return element?.OnCoerceMinimum((T)baseValue);
        }

        protected virtual object OnCoerceMinimum(T baseValue)
        {
            if (Comparer<T>.Default.Compare(baseValue, Maximum) > 0)
                baseValue = Maximum;
            return baseValue;
        }


        public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register(
            nameof(Maximum), typeof(T), typeof(NumericUpDownGeneric<T>), new PropertyMetadata(default(T), null, CoerceMaximumCallback));

        public T Maximum
        {
            get { return (T)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        private static object CoerceMaximumCallback(DependencyObject d, object baseValue)
        {
            var element = d as NumericUpDownGeneric<T>;
            return element?.OnCoerceMaximum((T)baseValue);
        }

        protected virtual object OnCoerceMaximum(T baseValue)
        {
            if (Comparer<T>.Default.Compare(baseValue, Minimum) < 0)
                baseValue = Minimum;
            return baseValue;
        }


        public static readonly DependencyProperty StepProperty = DependencyProperty.Register(
            nameof(Step), typeof(T), typeof(NumericUpDownGeneric<T>), new PropertyMetadata(default(T), null, CoerceStepCallback));

        public T Step
        {
            get { return (T)GetValue(StepProperty); }
            set { SetValue(StepProperty, value); }
        }

        private static object CoerceStepCallback(DependencyObject d, object baseValue)
        {
            var element = d as NumericUpDownGeneric<T>;
            return element?.OnCoerceStep((T)baseValue);
        }

        protected virtual object OnCoerceStep(T baseValue)
        {
            if (Comparer<T>.Default.Compare(baseValue, default(T)) <= 0)
                return 1;
            return baseValue;
        }


        public static readonly DependencyProperty DecimalPlacesProperty = DependencyProperty.Register(
            nameof(DecimalPlaces), typeof(int), typeof(NumericUpDownGeneric<T>), new PropertyMetadata(default(int), null, CoerceDecimalPlacesCallback), ValidateDecimalPlacesCallback);

        public int DecimalPlaces
        {
            get { return (int)GetValue(DecimalPlacesProperty); }
            set { SetValue(DecimalPlacesProperty, value); }
        }

        private static object CoerceDecimalPlacesCallback(DependencyObject d, object baseValue)
        {
            var element = d as NumericUpDownGeneric<T>;
            return element?.OnCoerceDecimalPlaces((int)baseValue);
        }

        protected virtual object OnCoerceDecimalPlaces(int baseValue)
        {
            return baseValue;
        }

        private static bool ValidateDecimalPlacesCallback(object value)
        {
            var val = (int)value;
            return val >= 0;
        }

        #endregion

        private void UpdateText()
        {
            int caret = -1;
            if (_inputTextBox != null)
                caret = _inputTextBox.CaretIndex;
            Text = Value?.ToString("0" + (DecimalPlaces > 0 ? "." + new string('0', DecimalPlaces) : string.Empty), CultureInfo.CurrentCulture) ?? string.Empty;
            if (_inputTextBox != null)
            {
                _inputTextBox.Text = Text;
                _inputTextBox.CaretIndex = caret >= 0 ? caret : Text.Length;
            }
        }

        protected override object OnCoerceText(string baseValue)
        {
            if (DecimalPlaces > 0)
            {
                var index = baseValue.IndexOf(DecimalSeparator, StringComparison.Ordinal);
                if (index >= 0)
                    baseValue = baseValue.Substring(0, index + DecimalSeparator.Length) + baseValue.Substring(index + DecimalSeparator.Length, DecimalPlaces);
            }
            return baseValue;
        }

        private void InputTextBoxOnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
                Increment();
            else
                Decrement();
            e.Handled = true;
        }

        private void InputTextBoxOnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            IsUpdatingText = false;
            var textBox = (TextBox)sender;
            var caret = textBox.CaretIndex;
            var currentText = textBox.Text;
            var input = e.Text;
            if (input == NegativeSign)
            {
                if (Comparer<T>.Default.Compare(Minimum, default(T)) >= 0 || currentText.StartsWith(NegativeSign) || caret != 0)
                    e.Handled = true;
                IsUpdatingText = true;
                return;
            }
            if (input == DecimalSeparator)
            {
                if (DecimalPlaces == 0 || caret == 0 ||
                    (currentText.StartsWith(NegativeSign) && caret < NegativeSign.Length + DecimalSeparator.Length) ||
                    caret < DecimalSeparator.Length || currentText.Contains(DecimalSeparator))
                {
                    e.Handled = true;
                    if (caret < currentText.Length && currentText[caret].ToString() == DecimalSeparator)
                        textBox.CaretIndex = caret + 1;
                }
                IsUpdatingText = true;
                return;
            }
            if (input == "0" && Text.Contains(DecimalSeparator) && caret > Text.IndexOf(DecimalSeparator, StringComparison.Ordinal))
            {
                IsUpdatingText = true;
                return;
            }
            if (input.Any(chr => !char.IsDigit(chr)))
                e.Handled = true;
        }

        private void InputTextBoxOnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsUpdatingValue)
                return;
            if (IsUpdatingTextBox)
            {
                IsUpdatingTextBox = false;
                return;
            }
            var textBox = (TextBox)sender;
            var text = textBox.Text ?? string.Empty;
            if (IsUpdatingText)
            {
                IsUpdatingText = false;
                var tmp = Text;
                Text = text;
                if (tmp == Text && tmp != text)
                {
                    IsUpdatingTextBox = true;
                    var caret = textBox.CaretIndex;
                    textBox.Text = Text;
                    textBox.CaretIndex = caret;
                }
                return;
            }
            if (ValidateText(text))
                UpdateValue(text);
            UpdateText();
        }

        private void InputTextBoxOnPasting(object sender, DataObjectPastingEventArgs e)
        {
            var textBox = (TextBox)sender;
            string text;
            if (!e.SourceDataObject.GetDataPresent(DataFormats.UnicodeText, true) || (text = e.SourceDataObject.GetData(DataFormats.UnicodeText) as string) == null)
            {
                e.Handled = true;
                e.CancelCommand();
                return;
            }
            var originalText = textBox.Text;
            var v = originalText.Remove(textBox.SelectionStart, textBox.SelectionLength);
            v = v.Insert(textBox.CaretIndex, text);
            if (!ValidateText(v))
            {
                e.Handled = true;
                e.CancelCommand();
            }
        }
    }
}
