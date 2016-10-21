using System;
using System.Windows.Controls;

namespace WpfConverters.Converters.Format
{
   public class HorzontalLineConverter: BaseValueConverterExtension
    {
        public override Object Convert(Object value, Type targetType, Object parameter, System.Globalization.CultureInfo culture)
        {
            TreeViewItem item = (TreeViewItem)value;
            ItemsControl ic = ItemsControl.ItemsControlFromItemContainer(item);
            var index = ic.ItemContainerGenerator.IndexFromContainer(item);

            if ((string)parameter == "left")
            {
                return index == 0 ? 0 : 1;
            }

            return index == ic.Items.Count - 1 ? 0 : 1;
        }
    }
}
