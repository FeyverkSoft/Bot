using System.Windows;
using System.Windows.Input;

namespace WpfConverters.Behaviors
{
    public static class ContextMenu
    {
        public static readonly DependencyProperty OpenedCommandProperty = DependencyProperty.RegisterAttached(
            "OpenedCommand", typeof(ICommand), typeof(ContextMenu), new PropertyMetadata(default(ICommand), OpenedCommandChangedCallback));

        private static void OpenedCommandChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var contextMenu = dependencyObject as System.Windows.Controls.ContextMenu;
            if (contextMenu == null)
                return;

            contextMenu.Opened -= ContextMenuOnOpened;
            if (e.NewValue is ICommand)
                contextMenu.Opened += ContextMenuOnOpened;
        }

        private static void ContextMenuOnOpened(object sender, RoutedEventArgs e)
        {
            var contextMenu = (System.Windows.Controls.ContextMenu)sender;
            var command = GetOpenedCommand(contextMenu);
            var commandParameter = GetOpenedCommandParameter(contextMenu);
            if (command?.CanExecute(commandParameter) == true)
                command.Execute(commandParameter);
        }

        public static void SetOpenedCommand(DependencyObject element, ICommand value)
        {
            element.SetValue(OpenedCommandProperty, value);
        }

        public static ICommand GetOpenedCommand(DependencyObject element)
        {
            return (ICommand)element.GetValue(OpenedCommandProperty);
        }


        public static readonly DependencyProperty OpenedCommandParameterProperty = DependencyProperty.RegisterAttached(
            "OpenedCommandParameter", typeof(object), typeof(ContextMenu), new PropertyMetadata(default(object)));

        public static void SetOpenedCommandParameter(DependencyObject element, object value)
        {
            element.SetValue(OpenedCommandParameterProperty, value);
        }

        public static object GetOpenedCommandParameter(DependencyObject element)
        {
            return element.GetValue(OpenedCommandParameterProperty);
        }


        public static readonly DependencyProperty ClosedCommandProperty = DependencyProperty.RegisterAttached(
            "ClosedCommand", typeof(ICommand), typeof(ContextMenu), new PropertyMetadata(default(ICommand), ClosedCommandChangedCallback));

        private static void ClosedCommandChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var contextMenu = dependencyObject as System.Windows.Controls.ContextMenu;
            if (contextMenu == null)
                return;

            contextMenu.Closed -= ContextMenuOnClosed;
            if (e.NewValue is ICommand)
                contextMenu.Closed += ContextMenuOnClosed;
        }

        private static void ContextMenuOnClosed(object sender, RoutedEventArgs e)
        {
            var contextMenu = (System.Windows.Controls.ContextMenu)sender;
            var command = GetClosedCommand(contextMenu);
            var commandParameter = GetClosedCommandParameter(contextMenu);
            if (command?.CanExecute(commandParameter) == true)
                command.Execute(commandParameter);
        }

        public static void SetClosedCommand(DependencyObject element, ICommand value)
        {
            element.SetValue(ClosedCommandProperty, value);
        }

        public static ICommand GetClosedCommand(DependencyObject element)
        {
            return (ICommand)element.GetValue(ClosedCommandProperty);
        }


        public static readonly DependencyProperty ClosedCommandParameterProperty = DependencyProperty.RegisterAttached(
            "ClosedCommandParameter", typeof(object), typeof(ContextMenu), new PropertyMetadata(default(object)));

        public static void SetClosedCommandParameter(DependencyObject element, object value)
        {
            element.SetValue(ClosedCommandParameterProperty, value);
        }

        public static object GetClosedCommandParameter(DependencyObject element)
        {
            return element.GetValue(ClosedCommandParameterProperty);
        }
    }
}
