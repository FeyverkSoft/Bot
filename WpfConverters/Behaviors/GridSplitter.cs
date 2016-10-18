using System.Windows;
using System.Windows.Controls;

namespace WpfConverters.Behaviors
{
    public static class GridSplitter
    {
        public static readonly DependencyProperty SplitterTargetProperty = DependencyProperty.RegisterAttached(
            "SplitterTarget", typeof(Grid), typeof(GridSplitter), new PropertyMetadata(default(Grid), SplitterTargetChangedCallback));

        public static void SetSplitterTarget(DependencyObject element, Grid value)
        {
            element.SetValue(SplitterTargetProperty, value);
        }

        public static Grid GetSplitterTarget(DependencyObject element)
        {
            return (Grid)element.GetValue(SplitterTargetProperty);
        }

        private static void SplitterTargetChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            TargetChanged(dependencyObject);
        }

        private static void TargetChanged(DependencyObject dependencyObject)
        {
            var source = dependencyObject as System.Windows.Controls.Primitives.ToggleButton;
            if (source == null)
                return;

            var target = GetSplitterTarget(source);
            if (target == null)
            {
                source.Checked -= SourceOnChecked;
                source.Unchecked -= SourceOnUnchecked;
                source.Indeterminate -= SourceOnIndeterminate;
                return;
            }

            source.Checked += SourceOnChecked;
            source.Unchecked += SourceOnUnchecked;
            source.Indeterminate += SourceOnIndeterminate;

            Toggle(source);
        }

        private static void SourceOnChecked(object sender, RoutedEventArgs routedEventArgs)
        {
            Toggle(sender);
        }

        private static void SourceOnUnchecked(object sender, RoutedEventArgs routedEventArgs)
        {
            Toggle(sender);
        }

        private static void SourceOnIndeterminate(object sender, RoutedEventArgs routedEventArgs)
        {
            Toggle(sender);
        }

        private static void Toggle(object sender)
        {
            var element = sender as System.Windows.Controls.Primitives.ToggleButton;
            if (element == null)
                return;

            var grid = GetSplitterTarget(element);
            if (grid == null)
                return;

            var direction = GetResizeDirection(element);
            if (direction == GridResizeDirection.Auto)
                return;

            var index = GetResizeIndex(element);

            if (index < 0)
                return;

            if (direction == GridResizeDirection.Rows)
            {
                if (grid.RowDefinitions.Count <= index)
                    return;

                var rowDefinition = grid.RowDefinitions[index];
                if (element.IsChecked == null)
                    rowDefinition.Height = new GridLength(1, GridUnitType.Star);
                else if (element.IsChecked == true)
                    rowDefinition.Height = new GridLength(0, GridUnitType.Pixel);
                else
                    rowDefinition.Height = GridLength.Auto;

                return;
            }

            if (direction == GridResizeDirection.Columns)
            {
                if (grid.ColumnDefinitions.Count <= index)
                    return;

                var columnDefinition = grid.ColumnDefinitions[index];
                if (element.IsChecked == null)
                    columnDefinition.Width = new GridLength(1, GridUnitType.Star);
                else if (element.IsChecked == true)
                    columnDefinition.Width = new GridLength(0, GridUnitType.Pixel);
                else
                    columnDefinition.Width = GridLength.Auto;
            }
        }


        public static readonly DependencyProperty ResizeDirectionProperty = DependencyProperty.RegisterAttached(
            "ResizeDirection", typeof(GridResizeDirection), typeof(GridSplitter), new PropertyMetadata(default(GridResizeDirection), ResizeDirectionChangedCallback));

        public static void SetResizeDirection(DependencyObject element, GridResizeDirection value)
        {
            element.SetValue(ResizeDirectionProperty, value);
        }

        public static GridResizeDirection GetResizeDirection(DependencyObject element)
        {
            return (GridResizeDirection)element.GetValue(ResizeDirectionProperty);
        }

        private static void ResizeDirectionChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            TargetChanged(dependencyObject);
        }


        public static readonly DependencyProperty ResizeIndexProperty = DependencyProperty.RegisterAttached(
            "ResizeIndex", typeof(int), typeof(GridSplitter), new PropertyMetadata(default(int), ResizeIndexChangedCallback));

        public static void SetResizeIndex(DependencyObject element, int value)
        {
            element.SetValue(ResizeIndexProperty, value);
        }

        public static int GetResizeIndex(DependencyObject element)
        {
            return (int)element.GetValue(ResizeIndexProperty);
        }

        private static void ResizeIndexChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            TargetChanged(dependencyObject);
        }
    }
}
