using System;
using System.Windows;
using System.Windows.Controls;
using Core.ConfigEntity;
using WpfConverters.Controls.Impl;
using Core.Core;

namespace WpfExecutor.Control.ImplControl
{
    [TemplatePart(Name = "PART_Name", Type = typeof(TextBox))]
    [TemplatePart(Name = "PART_Path", Type = typeof(TextBox))]
    [TemplatePart(Name = "PART_Type", Type = typeof(TextBox))]
    public class SaveFileParamUserControl : BaseControl
    {
        static SaveFileParamUserControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SaveFileParamUserControl),
                new FrameworkPropertyMetadata(typeof(SaveFileParamUserControl)));
        }

        public event DependencyPropertyChangedEventHandler SaveFileParamPropChanged;

        private TextBox _name;
        private TextBox _path;
        private TextBox _type;

        public static readonly DependencyProperty SaveFileParamProperty = DependencyProperty.Register(
nameof(SaveFileParam), typeof(SaveFileParam), typeof(SizeUserControl), new PropertyMetadata(default(SaveFileParam), SaveFileParamChangedCallback));

        public static readonly DependencyProperty FileNameProperty = DependencyProperty.Register(
nameof(FileName), typeof(String), typeof(SizeUserControl), new PropertyMetadata(default(String)));
        public static readonly DependencyProperty PathProperty = DependencyProperty.Register(
nameof(Path), typeof(String), typeof(SizeUserControl), new PropertyMetadata(default(String)));
        public static readonly DependencyProperty TypeProperty = DependencyProperty.Register(
nameof(Type), typeof(String), typeof(SizeUserControl), new PropertyMetadata(default(String)));

        private static void SaveFileParamChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var d = dependencyObject as SaveFileParamUserControl;
            d?.OnSaveFileParamChanged(e.OldValue, e.NewValue);
        }

        public SaveFileParam SaveFileParam
        {
            get
            {
                var temp = (SaveFileParam)GetValue(SaveFileParamProperty);
                if (temp != null)
                    return temp;
                SaveFileParam = new SaveFileParam();
                return (SaveFileParam)GetValue(SaveFileParamProperty);
            }
            set { SetValue(SaveFileParamProperty, value); }
        }

        public String FileName
        {
            get { return SaveFileParam.Name; }
            set { SaveFileParam.Name = value; }
        }
        public String Path
        {
            get { return SaveFileParam.Path; }
            set { SaveFileParam.Path = value; }
        }

        public String Type
        {
            get { return SaveFileParam.Type; }
            set { SaveFileParam.Type = value; }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _name = GetTemplateChild("PART_Name") as TextBox;
            _path = GetTemplateChild("PART_Path") as TextBox;
            _type = GetTemplateChild("PART_Type") as TextBox;
            if (_name != null)
            {
                _name.Text = FileName;
                _name.TextChanged += NameOnTextChanged;
            }
            if (_path != null)
            {
                _path.Text = Path;
                _path.TextChanged += PathOnTextChanged;
            }
            if (_type != null)
            {
                _type.Text = Type;
                _type.TextChanged += TypeOnTextChanged;
            }
        }

        private void TypeOnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            if (_type.Text == Type) return;
            Type = _type.Text ?? String.Empty;
        }

        private void NameOnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            if (_name.Text == FileName) return;
            FileName = _name.Text ?? String.Empty;
        }

        private void PathOnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            if (_path.Text == Path) return;
            Path = _path.Text ?? String.Empty;
        }


        private void OnSaveFileParamChanged(Object oldValue, Object newValue)
        {
            if (newValue == null)
                newValue = new SaveFileParam();
            if (oldValue == newValue)
                return;
            SaveFileParam = (SaveFileParam)newValue;
            SaveFileParamPropChanged?.Invoke(this, new DependencyPropertyChangedEventArgs(SaveFileParamProperty, oldValue, newValue));
        }
    }
}


