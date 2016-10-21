using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfExecutor.Model.Error;

namespace WpfExecutor.Views.Error
{
    public partial class ExceptionWindow : Window
    {
        public ExceptionWindow()
        {
            InitializeComponent();

            Loaded += OnLoaded;
            Closing += (sender, e) =>
            {
                ((Window)sender).Hide();
                e.Cancel = true;
            };
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Loaded -= OnLoaded;

            MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
        }
        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            SizeChanged -= OnSizeChanged;

            FixHeight();
        }

        private void Expander_OnExpanded(object sender, RoutedEventArgs e)
        {
            MinHeight = 100;
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;

            SizeChanged += OnSizeChanged;

            SizeToContent = SizeToContent.Height;
        }

        private void Expander_OnCollapsed(object sender, RoutedEventArgs e)
        {
            MinHeight = 100;
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;

            SizeChanged += OnSizeChanged;

            SizeToContent = SizeToContent.Height;
        }

        private async void FixHeight()
        {
            await Task.Delay(100);

            MinHeight = ActualHeight;
            MaxHeight = ActualHeight;
        }

        public Boolean? ShowDialog(ExceptionViewModel viewModel)
        {
            DataContext = viewModel;
            return base.ShowDialog();
        }
    }
}
