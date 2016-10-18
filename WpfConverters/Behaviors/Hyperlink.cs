using System.Diagnostics;
using System.Windows;
using System.Windows.Navigation;

namespace WpfConverters.Behaviors
{
    public static class Hyperlink
    {
        public static readonly DependencyProperty ProcessNavigationProperty = DependencyProperty.RegisterAttached(
            "ProcessNavigation", typeof(bool), typeof(Hyperlink), new PropertyMetadata(default(bool), ProcessNavigationChangedCallback));

        private static void ProcessNavigationChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var target = dependencyObject as System.Windows.Documents.Hyperlink;
            if (target == null)
                return;
            var value = e.NewValue as bool? ?? false;
            if (value)
            {
                target.RequestNavigate += HyperlinkOnRequestNavigate;
            }
            else
            {
                target.RequestNavigate -= HyperlinkOnRequestNavigate;
            }
        }

        private static void HyperlinkOnRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            var processStartInfo = new ProcessStartInfo("explorer.exe", "\"" + e.Uri.AbsoluteUri + "\"");
            Process.Start(processStartInfo);
            e.Handled = true;
        }

        public static void SetProcessNavigation(DependencyObject element, bool value)
        {
            element.SetValue(ProcessNavigationProperty, value);
        }

        public static bool GetProcessNavigation(DependencyObject element)
        {
            return (bool)element.GetValue(ProcessNavigationProperty);
        }
    }
}
