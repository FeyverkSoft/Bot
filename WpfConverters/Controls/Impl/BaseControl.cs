using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;

namespace WpfConverters.Controls.Impl
{
    [TemplatePart(Name = "PART_ClearButton", Type = typeof(ButtonBase))]
    public class BaseControl : Control
    {
        private ButtonBase _clearButton;

        protected BaseControl()
        {
            Language = XmlLanguage.GetLanguage(CultureInfo.CurrentUICulture.IetfLanguageTag);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _clearButton = GetTemplateChild("PART_ClearButton") as ButtonBase;
            if (_clearButton != null)
                _clearButton.Click += ClearButtonOnClick;
        }

        private void ClearButtonOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            OnClear();
        }

        protected virtual void OnClear()
        {
        }

        /// <summary>
        /// Указывает обязательно элемент должен содержать выбранное значение или нет
        /// </summary>
        public static readonly DependencyProperty IsRequiredProperty = DependencyProperty.Register(nameof(IsRequired),
            typeof(Boolean), typeof(BaseControl),
            new FrameworkPropertyMetadata(false));


        /// <summary>
        /// Указывает обязательно элемент должен содердать содержать значение или нет
        /// </summary>
        public Boolean IsRequired
        {
            get { return (Boolean)GetValue(IsRequiredProperty); }
            set { SetValue(IsRequiredProperty, value); }
        }
    }
}
