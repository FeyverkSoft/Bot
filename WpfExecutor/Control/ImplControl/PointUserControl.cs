using System;
using System.Windows;
using WpfConverters.Controls.Impl;
using Point = Core.Core.Point;

namespace WpfExecutor.Control.ImplControl
{
    [TemplatePart(Name = "PART_X", Type = typeof(Int32UpDown))]
    [TemplatePart(Name = "PART_Y", Type = typeof(Int32UpDown))]
    public class PointUserControl : BaseControl
    {
        static PointUserControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PointUserControl),
                new FrameworkPropertyMetadata(typeof(PointUserControl)));
        }

        public event DependencyPropertyChangedEventHandler XChanged;
        public event DependencyPropertyChangedEventHandler YChanged;
        public event DependencyPropertyChangedEventHandler PointChanged;

        private Int32UpDown _xDown;
        private Int32UpDown _yDown;


        public static readonly DependencyProperty XProperty = DependencyProperty.Register(
    nameof(X), typeof(Int32), typeof(PointUserControl), new PropertyMetadata(default(Int32), XChangedCallback));
        public static readonly DependencyProperty YProperty = DependencyProperty.Register(
    nameof(Y), typeof(Int32), typeof(PointUserControl), new PropertyMetadata(default(Int32), YChangedCallback));
        public static readonly DependencyProperty PointProperty = DependencyProperty.Register(
nameof(Point), typeof(Point), typeof(PointUserControl), new PropertyMetadata(default(Point), PointChangedCallback));

        private static void PointChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var d = dependencyObject as PointUserControl;
            d?.OnPointChanged(e.OldValue, e.NewValue);
        }

        public Int32 X
        {
            get { return Point.X; }
            set { Point.X = value; }
        }
        public Int32 Y
        {
            get { return Point.Y; }
            set { Point.Y = value; }
        }

        public Point Point
        {
            get
            {
                var temp = (Point)GetValue(PointProperty);
                if (temp != null)
                    return temp;
                Point = new Point();
                return (Point)GetValue(PointProperty);
            }
            set { SetValue(PointProperty, value); }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _xDown = GetTemplateChild("PART_X") as Int32UpDown;
            _yDown = GetTemplateChild("PART_Y") as Int32UpDown;
            if (_xDown != null)
            {
                _xDown.Value = X;
                _xDown.ValueChanged += PartXOnValueChanged;
            }
            if (_yDown != null)
            {
                _yDown.Value = Y;
                _yDown.ValueChanged += PartYOnValueChanged;
            }
        }

        private void PartXOnValueChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (_xDown.Value == X) return;
            X = _xDown.Value ?? 0;
        }

        private void PartYOnValueChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (_yDown.Value == Y) return;
            Y = _yDown.Value ?? 0;
        }

        private static void XChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var d = dependencyObject as PointUserControl;
            d?.OnXChanged(e.OldValue, e.NewValue);
        }

        private void OnXChanged(object oldValue, object newValue)
        {
            if (_xDown != null && _xDown.Value != X)
                _xDown.Value = X;
            Point.X = X;
            XChanged?.Invoke(this, new DependencyPropertyChangedEventArgs(XProperty, oldValue, newValue));
        }


        private static void YChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var d = dependencyObject as PointUserControl;
            d?.OnYChanged(e.OldValue, e.NewValue);
        }

        private void OnYChanged(object oldValue, object newValue)
        {
            if (_yDown != null && _yDown.Value != Y)
                _yDown.Value = Y;
            Point.Y = Y;
            YChanged?.Invoke(this, new DependencyPropertyChangedEventArgs(YProperty, oldValue, newValue));
        }

        private void OnPointChanged(object oldValue, object newValue)
        {
            if (newValue == null)
                newValue = new Point();
            if (oldValue == newValue)
                return;
            Point = (Point)newValue;
            PointChanged?.Invoke(this, new DependencyPropertyChangedEventArgs(PointProperty, oldValue, newValue));
        }
    }
}


