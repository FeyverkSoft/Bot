using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfConverters.Controls.Impl
{
    public class CustomTreeView : TreeView
    {
        public static readonly DependencyProperty SelectedObjectProperty =
            DependencyProperty.Register(nameof(SelectedObject), typeof(object), typeof(CustomTreeView), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public object SelectedObject
        {
            get { return GetValue(SelectedObjectProperty); }
            set { SetValue(SelectedObjectProperty, value); }
        }

        public static readonly DependencyProperty SelectedObjectTypeProperty =
            DependencyProperty.Register(nameof(SelectedObjectType), typeof(Type), typeof(CustomTreeView),
                new FrameworkPropertyMetadata(defaultValue: null));

        public Type SelectedObjectType
        {
            get { return (Type)GetValue(SelectedObjectTypeProperty); }
            set { SetValue(SelectedObjectTypeProperty, value); }
        }

        protected override void OnSelectedItemChanged(RoutedPropertyChangedEventArgs<object> e)
        {
            base.OnSelectedItemChanged(e);
            e.Handled = true;
            if (SelectedObjectType == null || SelectedObjectType == SelectedItem.GetType())
                SelectedObject = e.NewValue;
        }

        protected override void OnPreviewMouseRightButtonDown(MouseButtonEventArgs e)
        {
            var treeViewItem = VisualUpwardSearch(e.OriginalSource as DependencyObject);

            if (treeViewItem != null)
            {
                treeViewItem.Focus();
                e.Handled = true;
            }
        }

        private static TreeViewItem VisualUpwardSearch(DependencyObject source)
        {
            while (source != null && !(source is TreeViewItem))
                source = VisualTreeHelper.GetParent(source);

            return source as TreeViewItem;
        }
    }
}
