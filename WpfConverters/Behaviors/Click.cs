using System.Windows;
using System.Windows.Input;

namespace WpfConverters.Behaviors
{
    public static class Click
    {
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached(
            "Command", typeof(ICommand), typeof(Click), new UIPropertyMetadata(CommandChangedCallback));

        public static void SetCommand(DependencyObject element, ICommand value)
        {
            element.SetValue(CommandProperty, value);
        }

        public static ICommand GetCommand(DependencyObject element)
        {
            return (ICommand)element.GetValue(CommandProperty);
        }


        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.RegisterAttached(
            "CommandParameter", typeof(object), typeof(Click), new UIPropertyMetadata(null));

        public static void SetCommandParameter(DependencyObject element, object value)
        {
            element.SetValue(CommandParameterProperty, value);
        }

        public static object GetCommandParameter(DependencyObject element)
        {
            return element.GetValue(CommandParameterProperty);
        }


        private static void CommandChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var element = dependencyObject as FrameworkElement;
            if (element != null)
            {
                element.RemoveHandler(UIElement.MouseDownEvent, new MouseButtonEventHandler(OnMouseDown));
                element.AddHandler(UIElement.MouseDownEvent, new MouseButtonEventHandler(OnMouseDown));
            }
        }

        private static void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Left || e.ClickCount != 1)
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
