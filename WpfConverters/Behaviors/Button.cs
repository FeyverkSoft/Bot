using System.Windows;
using System.Windows.Input;

namespace WpfConverters.Behaviors
{
    public static class Button
    {
        public static readonly DependencyProperty ClickedCommandProperty = DependencyProperty.RegisterAttached(
            "ClickedCommand", typeof(ICommand), typeof(Button), new PropertyMetadata(default(ICommand), ClickedCommandChangedCallback));

        private static void ClickedCommandChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var button = dependencyObject as System.Windows.Controls.Button;
            if (button == null)
                return;

            button.Click -= ButtonOnClick;
            if (e.NewValue is ICommand)
                button.Click += ButtonOnClick;
        }

        private static void ButtonOnClick(object sender, RoutedEventArgs e)
        {
            var button = (System.Windows.Controls.Button)sender;
            var command = GetClickedCommand(button);
            var commandParameter = GetClickedCommandParameter(button);

            if (command?.CanExecute(commandParameter) == true)
            {
                command.Execute(commandParameter);
            }
        }

        public static void SetClickedCommand(DependencyObject element, ICommand value)
        {
            element.SetValue(ClickedCommandProperty, value);
        }

        public static ICommand GetClickedCommand(DependencyObject element)
        {
            return (ICommand)element.GetValue(ClickedCommandProperty);
        }


        public static readonly DependencyProperty ClickedCommandParameterProperty = DependencyProperty.RegisterAttached(
            "ClickedCommandParameter", typeof(object), typeof(Button), new PropertyMetadata(default(object)));

        public static void SetClickedCommandParameter(DependencyObject element, object value)
        {
            element.SetValue(ClickedCommandParameterProperty, value);
        }

        public static object GetClickedCommandParameter(DependencyObject element)
        {
            return element.GetValue(ClickedCommandParameterProperty);
        }
    }
}
