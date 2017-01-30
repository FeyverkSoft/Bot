using System;
using System.Windows;
using System.Windows.Controls;
using WpfConverters.Controls.Impl;
using WpfExecutor.Extensions.Localization;

namespace WpfExecutor.Control.ImplControl
{
    [TemplatePart(Name = "PART_Path", Type = typeof(TextBox))]
    [TemplatePart(Name = "PART_Button", Type = typeof(Button))]
    public class FolderPathUserControl : BaseControl
    {
        static FolderPathUserControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FolderPathUserControl),
                new FrameworkPropertyMetadata(typeof(FolderPathUserControl)));
        }

        public event DependencyPropertyChangedEventHandler FolderPathChanged;

        private TextBox _path;
        private Button _button;


        public static readonly DependencyProperty FolderPathProperty = DependencyProperty.Register(
    nameof(FolderPath), typeof(String), typeof(FolderPathUserControl), new PropertyMetadata(default(String), FolderPathChangedCallback));


        public String FolderPath
        {
            get { return (String)GetValue(FolderPathProperty); }
            set { SetValue(FolderPathProperty, value); }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _path = GetTemplateChild("PART_Path") as TextBox;
            _button = GetTemplateChild("PART_Button") as Button;
            if (_path != null)
            {
                _path.Text = FolderPath;
                _path.TextChanged += _path_TextChanged;
            }
            if (_button != null)
            {
                _button.Click += _button_Click;
            }
        }

        private void _path_TextChanged(Object sender, TextChangedEventArgs e)
        {
            var box = sender as TextBox;
            if (box?.Text != null && box?.Text != FolderPath)
                FolderPath = box?.Text;
        }

        private void _button_Click(Object sender, RoutedEventArgs e)
        {
            var open = new System.Windows.Forms.FolderBrowserDialog {Description = LocalizationManager.GetString("SelectFolder")};
            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FolderPath = open.SelectedPath;
            }
        }


        private static void FolderPathChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var d = dependencyObject as FolderPathUserControl;
            d?.OnFolderChanged(e.OldValue, e.NewValue);
        }

        private void OnFolderChanged(object oldValue, object newValue)
        {
            if (oldValue != newValue)
            {
                FolderPath = (String)newValue;
            }
            FolderPathChanged?.Invoke(this, new DependencyPropertyChangedEventArgs(FolderPathProperty, oldValue, newValue));
        }
    }
}


