using System;
using System.Windows.Controls;

namespace WpfConverters.Converters.Format
{
    public class VerticalLineConverter : BaseValueConverterExtension
    {
        public override Object Convert(Object value, Type targetType, Object parameter, System.Globalization.CultureInfo culture)
        {
            TreeViewItem item = (TreeViewItem)value;
            ItemsControl ic = ItemsControl.ItemsControlFromItemContainer(item);
            //var index = ic.ItemContainerGenerator.IndexFromContainer(item);

            if ((String)parameter != "top")
                return item.HasItems == false ? 0 : 1;

            return ic is TreeView ? 0 : 1;
        }
    }
}
