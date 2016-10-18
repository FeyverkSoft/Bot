using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace WpfConverters.Converters
{
    [MarkupExtensionReturnType(typeof(IValueConverter))]
    public abstract class BaseValueConverterExtension : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
