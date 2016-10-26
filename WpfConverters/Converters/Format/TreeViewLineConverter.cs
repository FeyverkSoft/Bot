using System;
using System.Windows.Controls;

namespace WpfConverters.Converters.Format
{
    public class TreeViewLineConverter : BaseValueConverterExtension
    {
        public override Object Convert(Object value, Type targetType, Object parameter, System.Globalization.CultureInfo culture)
        {
            TreeViewItem item = (TreeViewItem)value;
            ItemsControl ic = ItemsControl.ItemsControlFromItemContainer(item);
            return ic.ItemContainerGenerator.IndexFromContainer(item) == ic.Items.Count - 1;
        }

        public override Object ConvertBack(Object value, Type targetType, Object parameter, System.Globalization.CultureInfo culture)
        {
            return false;
        }
    }
}
