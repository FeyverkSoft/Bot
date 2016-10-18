using System;
using System.Windows;

namespace WpfConverters.Behaviors
{
    public static class DataGridColumn
    {
        public static readonly DependencyProperty ColumnTypeProperty = DependencyProperty.RegisterAttached(
            "ColumnType", typeof(Type), typeof(DataGridColumn), new PropertyMetadata(default(Type)));

        public static void SetColumnType(DependencyObject element, Type value)
        {
            element.SetValue(ColumnTypeProperty, value);
        }

        public static Type GetColumnType(DependencyObject element)
        {
            return (Type)element.GetValue(ColumnTypeProperty);
        }
    }
}
