using System;
using System.Windows;

namespace WpfConverters.Behaviors
{
    public static class TreeView
    {
        public static readonly DependencyProperty TrackSelectionChangingProperty = DependencyProperty.RegisterAttached(
            "TrackSelectionChanging", typeof(bool), typeof(TreeView), new PropertyMetadata(default(bool), TrackSelectionChangedChangedCallback));

        private static void TrackSelectionChangedChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var element = dependencyObject as System.Windows.Controls.TreeView;
            if (element == null)
                return;

            var value = e.NewValue as bool? ?? false;

            if (!value)
            {
                element.SelectedItemChanged -= ElementOnSelectedItemChanged;
            }
            else
            {
                element.SelectedItemChanged += ElementOnSelectedItemChanged;
            }
        }

        private static void ElementOnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var element = sender as System.Windows.Controls.TreeView;
            if (element == null)
                return;

            var type = GetSelectedItemType(element);
            var checkType = GetCheckSelectedItemType(element);
            if (!checkType || type != null)
            {
                if (element.SelectedItem != null && (!checkType || element.SelectedItem.GetType() == type))
                {
                    SetSelectedItem(element, element.SelectedItem);
                    SetSelectedValue(element, element.SelectedValue);
                }
                else
                {
                    SetSelectedItem(element, null);
                    SetSelectedValue(element, null);
                }
            }
        }

        public static void SetTrackSelectionChanging(DependencyObject element, bool value)
        {
            element.SetValue(TrackSelectionChangingProperty, value);
        }

        public static bool GetTrackSelectionChanging(DependencyObject element)
        {
            return (bool)element.GetValue(TrackSelectionChangingProperty);
        }


        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.RegisterAttached(
            "SelectedItem", typeof(object), typeof(TreeView), new PropertyMetadata(default(object)));

        public static void SetSelectedItem(DependencyObject element, object value)
        {
            element.SetValue(SelectedItemProperty, value);
        }

        public static object GetSelectedItem(DependencyObject element)
        {
            return element.GetValue(SelectedItemProperty);
        }


        public static readonly DependencyProperty SelectedItemTypeProperty = DependencyProperty.RegisterAttached(
            "SelectedItemType", typeof(Type), typeof(TreeView), new PropertyMetadata(default(Type)));

        public static void SetSelectedItemType(DependencyObject element, Type value)
        {
            element.SetValue(SelectedItemTypeProperty, value);
        }

        public static Type GetSelectedItemType(DependencyObject element)
        {
            return (Type)element.GetValue(SelectedItemTypeProperty);
        }


        public static readonly DependencyProperty SelectedValueProperty = DependencyProperty.RegisterAttached(
            "SelectedValue", typeof(object), typeof(TreeView), new PropertyMetadata(default(object)));

        public static void SetSelectedValue(DependencyObject element, object value)
        {
            element.SetValue(SelectedValueProperty, value);
        }

        public static object GetSelectedValue(DependencyObject element)
        {
            return element.GetValue(SelectedValueProperty);
        }


        public static readonly DependencyProperty CheckSelectedItemTypeProperty = DependencyProperty.RegisterAttached(
            "CheckSelectedItemType", typeof(bool), typeof(TreeView), new PropertyMetadata(true));

        public static void SetCheckSelectedItemType(DependencyObject element, bool value)
        {
            element.SetValue(CheckSelectedItemTypeProperty, value);
        }

        public static bool GetCheckSelectedItemType(DependencyObject element)
        {
            return (bool)element.GetValue(CheckSelectedItemTypeProperty);
        }
    }
}
