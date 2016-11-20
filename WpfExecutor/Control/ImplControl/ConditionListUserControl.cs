using System;
using System.Windows;
using System.Windows.Controls;
using Core.ConfigEntity.ActionObjects;
using WpfConverters.Controls.Impl;
using WpfExecutor.Factories;
using WpfExecutor.Model.ConditionalEditor;

namespace WpfExecutor.Control.ImplControl
{
    [TemplatePart(Name = "PART_Button", Type = typeof(Button))]
    public class ConditionListUserControl : BaseControl
    {
        static ConditionListUserControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ConditionListUserControl),
                new FrameworkPropertyMetadata(typeof(ConditionListUserControl)));
        }

        public event DependencyPropertyChangedEventHandler ConditionListChanged;

        private Button _button;


        public static readonly DependencyProperty ConditionListProperty = DependencyProperty.Register(
    nameof(ConditionList), typeof(ConditionalsParam), typeof(ConditionListUserControl), 
    new PropertyMetadata(default(ConditionalsParam), ConditionListChangedCallback));


        public ConditionalsParam ConditionList
        {
            get { return (ConditionalsParam)GetValue(ConditionListProperty); }
            set { SetValue(ConditionListProperty, value); }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _button = GetTemplateChild("PART_Button") as Button;
            if (_button != null)
            {
                _button.Click += _button_Click;
            }
        }

        private void _button_Click(Object sender, RoutedEventArgs e)
        {
            var win = WindowFactory.CreateConditionalEditorWindow(ConditionList);
            if (win.ShowDialog() == true)
            {
                var vm = win.DataContext as ConditionalEditorViewModel;
                if (vm != null)
                    ConditionList = new ConditionalsParam
                    {
                        Params = vm.ConditionaParamlsList,
                        Type = vm.SelectedItem.Item1
                    };
            }
        }


        private static void ConditionListChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var d = dependencyObject as ConditionListUserControl;
            d?.OnConditionListChanged(e.OldValue, e.NewValue);
        }

        private void OnConditionListChanged(object oldValue, object newValue)
        {
            if (oldValue != newValue)
            {
                ConditionList = (ConditionalsParam)newValue;
            }
            ConditionListChanged?.Invoke(this, new DependencyPropertyChangedEventArgs(ConditionListProperty, oldValue, newValue));
        }
    }
}


