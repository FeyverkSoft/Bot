using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfConverters.Behaviors
{
    public static class DoubleClick
    {
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached(
            "Command", typeof(ICommand), typeof(DoubleClick), new UIPropertyMetadata(CommandChanged));

        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.RegisterAttached(
            "CommandParameter", typeof(object), typeof(DoubleClick), new UIPropertyMetadata(null));

        public static void SetCommand(DependencyObject element, ICommand value)
        {
            element.SetValue(CommandProperty, value);
        }

        public static ICommand GetCommand(DependencyObject element)
        {
            return (ICommand)element.GetValue(CommandProperty);
        }

        public static void SetCommandParameter(DependencyObject element, object value)
        {
            element.SetValue(CommandParameterProperty, value);
        }

        public static object GetCommandParameter(DependencyObject element)
        {
            return element.GetValue(CommandParameterProperty);
        }

        private static void CommandChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var control = dependencyObject as Control;
            if (control != null)
            {
                control.RemoveHandler(Control.MouseDoubleClickEvent, new RoutedEventHandler(OnMouseDoubleClick));
                control.AddHandler(Control.MouseDoubleClickEvent, new RoutedEventHandler(OnMouseDoubleClick));
                return;
            }
            var element = dependencyObject as FrameworkElement;
            if (element != null)
            {
                element.RemoveHandler(UIElement.MouseDownEvent, new MouseButtonEventHandler(OnMouseDown));
                element.AddHandler(UIElement.MouseDownEvent, new MouseButtonEventHandler(OnMouseDown));
            }
        }

        private static void OnMouseDoubleClick(object sender, RoutedEventArgs e)
        {
            var element = sender as FrameworkElement;

            var command = GetCommand(element);
            if (command == null)
                return;
            var commandParameter = GetCommandParameter(element);

            if (command.CanExecute(commandParameter))
                command.Execute(commandParameter);

            e.Handled = true;
        }

        private static void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Left || e.ClickCount != 2)
                return;

            var element = sender as FrameworkElement;

            var command = GetCommand(element);
            if (command == null)
                return;
            var commandParameter = GetCommandParameter(element);

            if (command.CanExecute(commandParameter))
                command.Execute(commandParameter);

            e.Handled = true;
        }
    }
}
