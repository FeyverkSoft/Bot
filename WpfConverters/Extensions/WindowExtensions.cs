using System.Linq;
using System.Windows;

namespace WpfConverters.Extensions
{
    public static class WindowExtensions
    {
        public static void SetOwner(this Window window)
        {
            try
            {
                window.Owner = Application.Current.MainWindow;
                return;
            }
            catch
            {
                // ignored
            }

            try
            {
                var mainWindow = Application.Current.Windows.Cast<Window>().FirstOrDefault();
                if (mainWindow != null)
                    window.Owner = mainWindow;
            }
            catch
            {
                // ignored
            }

        }
    }
}