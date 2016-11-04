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
    [TemplatePart(Name = "PART_Type", Type = typeof(ImageFileFormatControl))]
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
        private ImageFileFormatControl _type;

        public static readonly DependencyProperty SaveFileParamProperty = DependencyProperty.Register(
nameof(SaveFileParam), typeof(SaveFileParam), typeof(SaveFileParamUserControl), new FrameworkPropertyMetadata(default(SaveFileParam), SaveFileParamChangedCallback));

        public static readonly DependencyProperty FileNameProperty = DependencyProperty.Register(
nameof(FileName), typeof(String), typeof(SaveFileParamUserControl), new FrameworkPropertyMetadata(default(String)));

        public static readonly DependencyProperty PathProperty = DependencyProperty.Register(
nameof(Path), typeof(String), typeof(SaveFileParamUserControl), new FrameworkPropertyMetadata(default(String)));

        public static readonly DependencyProperty TypeProperty = DependencyProperty.Register(
nameof(Type), typeof(ImageFileFormat), typeof(SaveFileParamUserControl), new FrameworkPropertyMetadata(default(ImageFileFormat)));

        public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register(
nameof(IsOpen), typeof(Boolean), typeof(SaveFileParamUserControl),
new FrameworkPropertyMetadata(default(Boolean), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
nameof(TextInfo), typeof(String), typeof(SaveFileParamUserControl), new PropertyMetadata(default(String)));

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

        public Boolean IsOpen
        {
            get { return (Boolean)GetValue(IsOpenProperty); }
            set
            {
                SetValue(IsOpenProperty, value);
            }
        }

        public String TextInfo
        {
            get { return (String)GetValue(TextProperty); }
            set
            {
                SetValue(TextProperty, value);
            }
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

        public ImageFileFormat Type
        {
            get { return SaveFileParam.Type; }
            set { SaveFileParam.Type = value; }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _name = GetTemplateChild("PART_Name") as TextBox;
            _path = GetTemplateChild("PART_Path") as TextBox;
            _type = GetTemplateChild("PART_Type") as ImageFileFormatControl;
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
                _type.SelectedValue = Type;
                _type.SelectionChanged += TypeOnSelectionChanged;
            }
        }

        private void TypeOnSelectionChanged(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            if ((ImageFileFormat)((Tuple<String, Object>)_type.SelectedValue).Item2 == Type) return;
            Type = (ImageFileFormat)((Tuple<String, Object>)_type.SelectedValue).Item2;
            UpdatePrev();
        }

        private void UpdatePrev()
        {
            TextInfo = $"{SaveFileParam?.Path ?? ""}/{(String.IsNullOrEmpty(SaveFileParam?.Name) ? "{DataTime}" : SaveFileParam.Name)}.{SaveFileParam?.Type}";
        }


        private void NameOnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            if (_name.Text == FileName) return;
            FileName = _name.Text ?? String.Empty;
            UpdatePrev();
        }

        private void PathOnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            if (_path.Text == Path) return;
            Path = _path.Text ?? String.Empty;
            UpdatePrev();
        }


        private void OnSaveFileParamChanged(Object oldValue, Object newValue)
        {
            if (newValue == null)
                newValue = new SaveFileParam();
            if (oldValue == newValue)
                return;
            SaveFileParam = (SaveFileParam)newValue;
            SaveFileParamPropChanged?.Invoke(this, new DependencyPropertyChangedEventArgs(SaveFileParamProperty, oldValue, newValue));
            UpdatePrev();
        }
    }
}


