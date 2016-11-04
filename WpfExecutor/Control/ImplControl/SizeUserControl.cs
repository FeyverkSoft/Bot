using System;
using System.Windows;
using WpfConverters.Controls.Impl;
using Size = Core.Core.Size;

namespace WpfExecutor.Control.ImplControl
{
    [TemplatePart(Name = "PART_X", Type = typeof(Int32UpDown))]
    [TemplatePart(Name = "PART_Y", Type = typeof(Int32UpDown))]
    public class SizeUserControl : BaseControl
    {
        static SizeUserControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SizeUserControl),
                new FrameworkPropertyMetadata(typeof(SizeUserControl)));
        }

        public event DependencyPropertyChangedEventHandler WidthXChanged;
        public event DependencyPropertyChangedEventHandler HeightYChanged;
        public event DependencyPropertyChangedEventHandler SizePropChanged;

        private Int32UpDown _xDown;
        private Int32UpDown _yDown;


        public static readonly DependencyProperty XProperty = DependencyProperty.Register(
    nameof(WidthX), typeof(Int32), typeof(SizeUserControl), new PropertyMetadata(default(Int32), XChangedCallback));
        public static readonly DependencyProperty HeightYProperty = DependencyProperty.Register(
    nameof(HeightY), typeof(Int32), typeof(SizeUserControl), new PropertyMetadata(default(Int32), YChangedCallback));
        public static readonly DependencyProperty SizeProperty = DependencyProperty.Register(
nameof(Size), typeof(Size), typeof(SizeUserControl), new PropertyMetadata(default(Size), PointChangedCallback));

        private static void PointChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var d = dependencyObject as SizeUserControl;
            d?.OnPointChanged(e.OldValue, e.NewValue);
        }

        public Int32 WidthX
        {
            get { return Size.WidthX; }
            set { Size.WidthX = value; }
        }
        public Int32 HeightY
        {
            get { return Size.HeightY; }
            set { Size.HeightY = value; }
        }

        public Size Size
        {
            get
            {
               var temp = (Size) GetValue(SizeProperty);
                if (temp != null)
                    return temp;
                Size = new Size();
                return (Size)GetValue(SizeProperty);
            }
            set { SetValue(SizeProperty, value); }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _xDown = GetTemplateChild("PART_X") as Int32UpDown;
            _yDown = GetTemplateChild("PART_Y") as Int32UpDown;
            if (_xDown != null)
            {
                _xDown.Value = WidthX;
                _xDown.ValueChanged += PartXOnValueChanged;
            }
            if (_yDown != null)
            {
                _yDown.Value = HeightY;
                _yDown.ValueChanged += PartYOnValueChanged;
            }
        }

        private void PartXOnValueChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (_xDown.Value == WidthX) return;
            WidthX = _xDown.Value ?? 0;
        }

        private void PartYOnValueChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (_yDown.Value == HeightY) return;
            HeightY = _yDown.Value ?? 0;
        }

        private static void XChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var d = dependencyObject as SizeUserControl;
            d?.OnXChanged(e.OldValue, e.NewValue);
        }

        private void OnXChanged(object oldValue, object newValue)
        {
            if (_xDown != null && _xDown.Value != WidthX)
                _xDown.Value = WidthX;
            Size.WidthX= WidthX;
            WidthXChanged?.Invoke(this, new DependencyPropertyChangedEventArgs(XProperty, oldValue, newValue));
        }


        private static void YChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var d = dependencyObject as SizeUserControl;
            d?.OnYChanged(e.OldValue, e.NewValue);
        }

        private void OnYChanged(object oldValue, object newValue)
        {
            if (_yDown != null && _yDown.Value != HeightY)
                _yDown.Value = HeightY;
            Size.HeightY = HeightY;
            HeightYChanged?.Invoke(this, new DependencyPropertyChangedEventArgs(HeightYProperty, oldValue, newValue));
        }

        private void OnPointChanged(object oldValue, object newValue)
        {
            if(newValue == null)
                newValue = new Size();
            if (oldValue == newValue)
                return;
            Size = (Size)newValue;
            SizePropChanged?.Invoke(this, new DependencyPropertyChangedEventArgs(SizeProperty, oldValue, newValue));
        }
    }
}


