using System.Collections;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace WpfConverters.Behaviors
{
    public static class Element
    {
        public static readonly DependencyProperty InputBindingsProperty = DependencyProperty.RegisterAttached(
            "InputBindings", typeof(ICollection), typeof(Element), new PropertyMetadata(default(ICollection), InputBindingsChangedCallback));

        private static void InputBindingsChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var element = dependencyObject as UIElement;
            if (element == null)
                return;

            var inputBindings = e.NewValue as ICollection;
            element.InputBindings.Clear();
            if (inputBindings != null)
                element.InputBindings.AddRange(inputBindings.OfType<object>().SelectMany(x =>
                {
                    if (x is InputBinding)
                        return new[] { (InputBinding)x };
                    if (x is IEnumerable)
                        return ((IEnumerable)x).OfType<InputBinding>();
                    if (x is CollectionContainer)
                        return ((CollectionContainer)x).Collection?.OfType<InputBinding>() ?? Enumerable.Empty<InputBinding>();
                    return Enumerable.Empty<InputBinding>();
                }).Where(x => x != null).ToList());
        }

        public static void SetInputBindings(DependencyObject element, ICollection value)
        {
            element.SetValue(InputBindingsProperty, value);
        }

        public static ICollection GetInputBindings(DependencyObject element)
        {
            return (ICollection)element.GetValue(InputBindingsProperty);
        }


        public static readonly DependencyProperty LoadedCommandProperty = DependencyProperty.RegisterAttached(
            "LoadedCommand", typeof(ICommand), typeof(Element), new PropertyMetadata(default(ICommand), LoadedCommandChangedCallback));

        private static void LoadedCommandChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var element = dependencyObject as FrameworkElement;
            if (element == null)
                return;

            element.Loaded -= ElementOnLoaded;

            if (e.NewValue != null)
            {
                element.Loaded += ElementOnLoaded;
            }
        }

        private static void ElementOnLoaded(object sender, RoutedEventArgs e)
        {
            var element = (FrameworkElement)sender;
            var command = GetLoadedCommand(element);
            var parameter = GetLoadedCommandParameter(element);
            if (command.CanExecute(parameter))
                command.Execute(parameter);
        }

        public static void SetLoadedCommand(DependencyObject element, ICommand value)
        {
            element.SetValue(LoadedCommandProperty, value);
        }

        public static ICommand GetLoadedCommand(DependencyObject element)
        {
            return (ICommand)element.GetValue(LoadedCommandProperty);
        }


        public static readonly DependencyProperty LoadedCommandParameterProperty = DependencyProperty.RegisterAttached(
            "LoadedCommandParameter", typeof(object), typeof(Element), new PropertyMetadata(default(object)));

        public static void SetLoadedCommandParameter(DependencyObject element, object value)
        {
            element.SetValue(LoadedCommandParameterProperty, value);
        }

        public static object GetLoadedCommandParameter(DependencyObject element)
        {
            return (object)element.GetValue(LoadedCommandParameterProperty);
        }
    }
}
