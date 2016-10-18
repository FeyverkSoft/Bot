using System.Windows;
using System.Windows.Input;

namespace WpfConverters.Behaviors
{
    public static class TreeViewItem
    {
        public static readonly DependencyProperty ExpandedCommandProperty = DependencyProperty.RegisterAttached(
            "ExpandedCommand", typeof(ICommand), typeof(TreeViewItem), new PropertyMetadata(default(ICommand), ExpandedChangedCallback));

        private static void ExpandedChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var treeViewItem = dependencyObject as System.Windows.Controls.TreeViewItem;
            if (treeViewItem == null)
                return;

            treeViewItem.Expanded -= TreeViewItemOnExpanded;
            if (e.NewValue is ICommand)
                treeViewItem.Expanded += TreeViewItemOnExpanded;
        }

        private static void TreeViewItemOnExpanded(object sender, RoutedEventArgs e)
        {
            var treeViewItem = (System.Windows.Controls.TreeViewItem)sender;
            var command = GetExpandedCommand(treeViewItem);
            var commandParameter = GetExpandedCommandParameter(treeViewItem);

            if (command.CanExecute(commandParameter))
                command.Execute(commandParameter);
        }

        public static void SetExpandedCommand(DependencyObject element, ICommand value)
        {
            element.SetValue(ExpandedCommandProperty, value);
        }

        public static ICommand GetExpandedCommand(DependencyObject element)
        {
            return (ICommand)element.GetValue(ExpandedCommandProperty);
        }


        public static readonly DependencyProperty ExpandedCommandParameterProperty = DependencyProperty.RegisterAttached(
            "ExpandedCommandParameter", typeof(object), typeof(TreeViewItem), new PropertyMetadata(default(object)));

        public static void SetExpandedCommandParameter(DependencyObject element, object value)
        {
            element.SetValue(ExpandedCommandParameterProperty, value);
        }

        public static object GetExpandedCommandParameter(DependencyObject element)
        {
            return element.GetValue(ExpandedCommandParameterProperty);
        }
    }
}
