using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace WpfConverters.Converters
{
    [MarkupExtensionReturnType(typeof(IMultiValueConverter))]
    public abstract class BaseMultiValueConverterExtension : MarkupExtension, IMultiValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        public virtual object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        public virtual object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
