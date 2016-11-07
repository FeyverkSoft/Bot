using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using WpfConverters.Controls.Impl;

namespace WpfExecutor.Control.ImplControl
{
    [TemplatePart(Name = "PART_Path", Type = typeof(TextBox))]
    [TemplatePart(Name = "PART_Button", Type = typeof(Button))]
    public class FilePathUserControl : BaseControl
    {
        static FilePathUserControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FilePathUserControl),
                new FrameworkPropertyMetadata(typeof(FilePathUserControl)));
        }

        public event DependencyPropertyChangedEventHandler FilePathChanged;

        private TextBox _path;
        private Button _button;


        public static readonly DependencyProperty FilePathProperty = DependencyProperty.Register(
    nameof(FilePath), typeof(String), typeof(FilePathUserControl), new PropertyMetadata(default(String), FilePathChangedCallback));


        public String FilePath
        {
            get { return (String)GetValue(FilePathProperty); }
            set { SetValue(FilePathProperty, value); }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _path = GetTemplateChild("PART_Path") as TextBox;
            _button = GetTemplateChild("PART_Button") as Button;
            if (_path != null)
            {
                _path.Text = FilePath;
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
            if (box?.Text != null && box?.Text != FilePath)
                FilePath = box?.Text;
        }

        private void _button_Click(Object sender, RoutedEventArgs e)
        {
            var open = new OpenFileDialog();
            if (open.ShowDialog() == true)
            {
                FilePath = open.FileName;
            }
        }


        private static void FilePathChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var d = dependencyObject as FilePathUserControl;
            d?.OnFileChanged(e.OldValue, e.NewValue);
        }

        private void OnFileChanged(object oldValue, object newValue)
        {
            if (oldValue != newValue)
            {
                FilePath = (String) newValue;
            }
            FilePathChanged?.Invoke(this, new DependencyPropertyChangedEventArgs(FilePathProperty, oldValue, newValue));
        }
    }
}


