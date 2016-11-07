using System;
using System.Windows;
using System.Windows.Controls;
using Core.ConfigEntity;
using WpfConverters.Controls.Impl;
using Core.Core;

namespace WpfExecutor.Control.ImplControl
{
    [TemplatePart(Name = "PART_Name", Type = typeof(TextBox))]
    [TemplatePart(Name = "PART_Path", Type = typeof(FilePathUserControl))]
    [TemplatePart(Name = "PART_Type", Type = typeof(ImageFileFormatControl))]
    [TemplatePart(Name = "PART_SaveFile", Type = typeof(CheckBox))]
    public class SaveFileParamUserControl : BaseControl
    {
        static SaveFileParamUserControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SaveFileParamUserControl),
                new FrameworkPropertyMetadata(typeof(SaveFileParamUserControl)));
        }

        public event DependencyPropertyChangedEventHandler SaveFileParamPropChanged;
        public event DependencyPropertyChangedEventHandler SaveFilePropChanged;

        private TextBox _name;
        private FilePathUserControl _path;
        private ImageFileFormatControl _type;
        private CheckBox _saveFile;

        public static readonly DependencyProperty SaveFileParamProperty = DependencyProperty.Register(
nameof(SaveFileParam), typeof(SaveFileParam), typeof(SaveFileParamUserControl), new FrameworkPropertyMetadata(default(SaveFileParam), SaveFileParamChangedCallback));

        public static readonly DependencyProperty FileNameProperty = DependencyProperty.Register(
nameof(FileName), typeof(String), typeof(SaveFileParamUserControl), new FrameworkPropertyMetadata(default(String)));

        public static readonly DependencyProperty PathProperty = DependencyProperty.Register(
nameof(Path), typeof(String), typeof(SaveFileParamUserControl), new FrameworkPropertyMetadata(default(String)));

        public static readonly DependencyProperty TypeProperty = DependencyProperty.Register(
nameof(Type), typeof(ImageFileFormat), typeof(SaveFileParamUserControl), new FrameworkPropertyMetadata(default(ImageFileFormat)));

        public static readonly DependencyProperty SaveFileProperty = DependencyProperty.Register(
nameof(SaveFile), typeof(Boolean), typeof(SaveFileParamUserControl), new FrameworkPropertyMetadata(default(Boolean), SaveFileChangedCallback));


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

        public Boolean SaveFile
        {
            get { return SaveFileParam.SaveFile; }
            set { SaveFileParam.SaveFile = value; }
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
            _path = GetTemplateChild("PART_Path") as FilePathUserControl;
            _type = GetTemplateChild("PART_Type") as ImageFileFormatControl;
            _saveFile = GetTemplateChild("PART_SaveFile") as CheckBox;
            if (_name != null)
            {
                _name.Text = FileName;
                _name.TextChanged += NameOnTextChanged;
            }
            if (_path != null)
            {
                _path.FilePath = Path;
                _path.FilePathChanged += _path_FilePathChanged; ;
            }
            if (_type != null)
            {
                _type.SelectedValue = Type;
                _type.SelectionChanged += TypeOnSelectionChanged;
            }
            if (_saveFile != null)
            {
                _saveFile.IsChecked = SaveFile;
            }
        }

        private void _path_FilePathChanged(Object sender, DependencyPropertyChangedEventArgs e)
        {
            var fp = sender as FilePathUserControl;
            if (fp?.FilePath == Path) return;
            Path = fp?.FilePath ?? String.Empty;
            UpdatePrev();
        }

        private void _saveFile_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (_saveFile.IsChecked == SaveFile) return;
            SaveFile = _saveFile.IsChecked.Value;
            UpdatePrev();
        }
        private static void SaveFileChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var d = dependencyObject as SaveFileParamUserControl;
            d?.OnSaveFileChanged(e.OldValue, e.NewValue);
        }

        private void OnSaveFileChanged(Object oldValue, Object newValue)
        {
            if (newValue == null)
                newValue = false;
            if (oldValue == newValue)
                return;
            SaveFile = (Boolean)newValue;
            SaveFilePropChanged?.Invoke(this, new DependencyPropertyChangedEventArgs(SaveFileProperty, oldValue, newValue));
            UpdatePrev();
        }


        private void TypeOnSelectionChanged(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            if ((ImageFileFormat)((Tuple<String, Object>)_type.SelectedValue).Item2 == Type) return;
            Type = (ImageFileFormat)((Tuple<String, Object>)_type.SelectedValue).Item2;
            UpdatePrev();
        }

        private void UpdatePrev()
        {
            if (SaveFile)
                TextInfo =
                    $"{SaveFileParam?.Path ?? ""}/{(String.IsNullOrEmpty(SaveFileParam?.Name) ? "{DataTime}" : SaveFileParam.Name)}.{SaveFileParam?.Type}";
            else
                TextInfo = "NO";
        }


        private void NameOnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            if (_name.Text == FileName) return;
            FileName = _name.Text ?? String.Empty;
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


