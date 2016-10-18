using System;
using System.Globalization;
using System.Windows;
using System.Windows.Input;

namespace WpfConverters.Converters.Common
{
    public class CommandToVisibilityConverter : BaseValueConverterExtension
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var command = value as ICommand;
            if (command == null)
                return Visibility.Visible;

            var canExecute = command.CanExecute(parameter);
            return canExecute ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
