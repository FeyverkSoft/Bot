using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfConverters.Helpers;
using WpfConverters.Extensions;

namespace WpfConverters.Behaviors
{
    public static class ScrollViewer
    {
        #region Group scrolling

        private static readonly Dictionary<System.Windows.Controls.ScrollViewer, string> ScrollViewers = new Dictionary<System.Windows.Controls.ScrollViewer, string>();

        private static readonly Dictionary<string, double> HorizontalScrollOffsets = new Dictionary<string, double>();

        private static readonly Dictionary<string, double> VerticalScrollOffsets = new Dictionary<string, double>();

        public static readonly DependencyProperty ScrollGroupProperty = DependencyProperty.RegisterAttached(
            "ScrollGroup", typeof(string), typeof(ScrollViewer), new PropertyMetadata(default(string), ScrollGroupChangedCallback));

        public static void SetScrollGroup(DependencyObject element, string value)
        {
            element.SetValue(ScrollGroupProperty, value);
        }

        public static string GetScrollGroup(DependencyObject element)
        {
            return (string)element.GetValue(ScrollGroupProperty);
        }

        private static void ScrollGroupChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var scrollViewer = dependencyObject as System.Windows.Controls.ScrollViewer;
            if (scrollViewer != null)
            {
                if (!string.IsNullOrEmpty((string)e.OldValue))
                {
                    // Remove scrollviewer
                    if (ScrollViewers.ContainsKey(scrollViewer))
                    {
                        scrollViewer.ScrollChanged -= ScrollViewer_ScrollChanged;
                        ScrollViewers.Remove(scrollViewer);
                    }
                }

                if (!string.IsNullOrEmpty((string)e.NewValue))
                {
                    // If group already exists, set scrollposition of 
                    // new scrollviewer to the scrollposition of the group
                    if (HorizontalScrollOffsets.Keys.Contains((string)e.NewValue))
                    {
                        scrollViewer.ScrollToHorizontalOffset(
                                      HorizontalScrollOffsets[(string)e.NewValue]);
                    }
                    else
                    {
                        HorizontalScrollOffsets.Add((string)e.NewValue,
                                                scrollViewer.HorizontalOffset);
                    }

                    if (VerticalScrollOffsets.Keys.Contains((string)e.NewValue))
                    {
                        scrollViewer.ScrollToVerticalOffset(VerticalScrollOffsets[(string)e.NewValue]);
                    }
                    else
                    {
                        VerticalScrollOffsets.Add((string)e.NewValue, scrollViewer.VerticalOffset);
                    }

                    // Add scrollviewer
                    ScrollViewers.Add(scrollViewer, (string)e.NewValue);
                    scrollViewer.ScrollChanged += ScrollViewer_ScrollChanged;
                }
            }
        }

        private static void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.VerticalChange != 0 || e.HorizontalChange != 0)
            {
                var changedScrollViewer = sender as System.Windows.Controls.ScrollViewer;
                Scroll(changedScrollViewer);
            }
        }

        private static void Scroll(System.Windows.Controls.ScrollViewer changedScrollViewer)
        {
            var group = ScrollViewers[changedScrollViewer];
            VerticalScrollOffsets[group] = changedScrollViewer.VerticalOffset;
            HorizontalScrollOffsets[group] = changedScrollViewer.HorizontalOffset;

            foreach (var scrollViewer in ScrollViewers.Where(s => s.Value ==
                                              group && s.Key != changedScrollViewer))
            {
                if (scrollViewer.Key.VerticalOffset != changedScrollViewer.VerticalOffset)
                {
                    scrollViewer.Key.ScrollToVerticalOffset(changedScrollViewer.VerticalOffset);
                }

                if (scrollViewer.Key.HorizontalOffset != changedScrollViewer.HorizontalOffset)
                {
                    scrollViewer.Key.ScrollToHorizontalOffset(changedScrollViewer.HorizontalOffset);
                }
            }
        }

        #endregion

        #region End-to-end scrolling

        public static readonly DependencyProperty EndToEndScrollingProperty = DependencyProperty.RegisterAttached(
            "EndToEndScrolling", typeof(bool), typeof(ScrollViewer), new PropertyMetadata(default(bool), EndToEndScrollingChangedCallback));

        private static void EndToEndScrollingChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var element = dependencyObject as FrameworkElement;
            if (element == null)
                return;

            var value = e.NewValue as bool? ?? false;
            var scrollViewer = dependencyObject as System.Windows.Controls.ScrollViewer;
            if (scrollViewer == null)
            {
                scrollViewer = VisualHelper.FindVisualChild<System.Windows.Controls.ScrollViewer>(dependencyObject);
                if (scrollViewer == null)
                {
                    if (value == true)
                    {
                        element.Loaded += EndToEndScrollingElementOnLoaded;
                    }
                    return;
                }
            }

            var context = new ScrollViewerContext(scrollViewer, dependencyObject);
            context.PreviewMouseWheel -= EndToEndScrolling_ScrollViewerOnPreviewMouseWheel;
            if (value == true)
            {
                context.PreviewMouseWheel += EndToEndScrolling_ScrollViewerOnPreviewMouseWheel;
            }
        }

        private static void EndToEndScrollingElementOnLoaded(object sender, RoutedEventArgs e)
        {
            var element = (FrameworkElement)sender;
            element.Loaded -= EndToEndScrollingElementOnLoaded;

            SetEndToEndScrolling(element, false);
            SetEndToEndScrolling(element, true);
        }

        private static void EndToEndScrolling_ScrollViewerOnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var context = sender as ScrollViewerContext;
            if (context == null)
                return;
            var scrollViewer = context.ScrollViewer;

            var parent = VisualHelper.FindParent<System.Windows.Controls.ScrollViewer>(scrollViewer);
            if (parent == null)
                return;

            var horizontalScrolling = GetHorizontalScrolling(context.Source);
            var isCtrl = (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control;
            var isShift = (Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift;
            if ((!horizontalScrolling || !isCtrl) && !isShift)
            {
                var direction = GetMouseWheelDirection(e.Delta);
                var lineCount = SystemParameters.WheelScrollLines;
                if (direction == true)
                    parent.LineDown(lineCount);
                else if (direction == false)
                    parent.LineUp(lineCount);
            }
        }

        public static void SetEndToEndScrolling(DependencyObject element, bool value)
        {
            element.SetValue(EndToEndScrollingProperty, value);
        }

        public static bool GetEndToEndScrolling(DependencyObject element)
        {
            return (bool)element.GetValue(EndToEndScrollingProperty);
        }

        #endregion

        #region Horizontal scrolling

        public static readonly DependencyProperty HorizontalScrollingProperty = DependencyProperty.RegisterAttached(
            "HorizontalScrolling", typeof(bool), typeof(ScrollViewer), new PropertyMetadata(default(bool), HorizontalScrollingChangedCallback));

        private static void HorizontalScrollingChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var element = dependencyObject as FrameworkElement;
            if (element == null)
                return;

            var value = e.NewValue as bool? ?? false;
            var scrollViewer = dependencyObject as System.Windows.Controls.ScrollViewer;
            if (scrollViewer == null)
            {
                scrollViewer = VisualHelper.FindVisualChild<System.Windows.Controls.ScrollViewer>(dependencyObject);
                if (scrollViewer == null)
                {
                    if (value == true)
                    {
                        element.Loaded += HorizontalScrollingElementOnLoaded;
                    }
                    return;
                }
            }

            var context = new ScrollViewerContext(scrollViewer, dependencyObject);
            context.PreviewMouseWheel -= HorizontalScrolling_ScrollViewerOnPreviewMouseWheel;
            if (value == true)
            {
                context.PreviewMouseWheel += HorizontalScrolling_ScrollViewerOnPreviewMouseWheel;
            }
        }

        private static void HorizontalScrollingElementOnLoaded(object sender, RoutedEventArgs e)
        {
            var element = (FrameworkElement)sender;
            element.Loaded -= HorizontalScrollingElementOnLoaded;

            SetHorizontalScrolling(element, false);
            SetHorizontalScrolling(element, true);
        }

        private static void HorizontalScrolling_ScrollViewerOnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var context = sender as ScrollViewerContext;
            if (context == null)
                return;
            var scrollViewer = context.ScrollViewer;

            var isCtrl = (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control;
            var direction = GetMouseWheelDirection(e.Delta);
            var lineCount = SystemParameters.WheelScrollLines;
            if (isCtrl)
            {
                e.Handled = true;
                if (direction == true)
                    scrollViewer.LineRight(lineCount);
                else if (direction == false)
                    scrollViewer.LineLeft(lineCount);

                var endToEndScrolling = GetEndToEndScrolling(context.Source);
                if (endToEndScrolling == true)
                {
                    var parent = VisualHelper.FindParent<System.Windows.Controls.ScrollViewer>(scrollViewer);
                    if (parent != null)
                    {
                        if (direction == true)
                            parent.LineRight(lineCount);
                        else
                            parent.LineLeft(lineCount);
                    }
                }
            }
        }

        public static void SetHorizontalScrolling(DependencyObject element, bool value)
        {
            element.SetValue(HorizontalScrollingProperty, value);
        }

        public static bool GetHorizontalScrolling(DependencyObject element)
        {
            return (bool)element.GetValue(HorizontalScrollingProperty);
        }

        #endregion


        #region VerticalScrollOffset

        public static readonly DependencyProperty AttachScrollProperty = DependencyProperty.RegisterAttached(
            "AttachScroll", typeof(bool), typeof(ScrollViewer), new PropertyMetadata(default(bool), AttachScrollChangedCallback));

        private static void AttachScrollChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var element = dependencyObject as FrameworkElement;
            if (element == null)
                return;

            var value = e.NewValue as bool? ?? false;
            var scrollViewer = dependencyObject as System.Windows.Controls.ScrollViewer;
            if (scrollViewer == null)
            {
                scrollViewer = VisualHelper.FindVisualChild<System.Windows.Controls.ScrollViewer>(dependencyObject);
                if (scrollViewer == null)
                {
                    if (value == true)
                    {
                        element.Loaded += AttachScrollElementOnLoaded;
                    }
                    return;
                }
            }

            var context = new ScrollViewerContext(scrollViewer, dependencyObject);
            context.ScrollChanged -= AttachScroll_ScrollViewerOnScrollChanged;
            if (value == true)
            {
                context.ScrollChanged += AttachScroll_ScrollViewerOnScrollChanged;
                SetVerticalOffset(element, scrollViewer.ContentVerticalOffset);
                SetScrollableHeight(element, scrollViewer.ScrollableHeight);
            }
        }

        private static void AttachScrollElementOnLoaded(object sender, RoutedEventArgs e)
        {
            var element = (FrameworkElement)sender;
            element.Loaded -= AttachScrollElementOnLoaded;

            SetAttachScroll(element, false);
            SetAttachScroll(element, true);
        }

        private static void AttachScroll_ScrollViewerOnScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            var context = (ScrollViewerContext) sender;
            var scrollViewer = context.ScrollViewer;
            var element = context.Source;

            SetVerticalOffset(element, scrollViewer.ContentVerticalOffset);
            SetScrollableHeight(element, scrollViewer.ScrollableHeight);
        }

        public static void SetAttachScroll(DependencyObject element, bool value)
        {
            element.SetValue(AttachScrollProperty, value);
        }

        public static bool GetAttachScroll(DependencyObject element)
        {
            return (bool)element.GetValue(AttachScrollProperty);
        }


        public static readonly DependencyProperty VerticalOffsetProperty = DependencyProperty.RegisterAttached(
            "VerticalOffset", typeof(double), typeof(ScrollViewer), new PropertyMetadata(default(double)));

        public static void SetVerticalOffset(DependencyObject element, double value)
        {
            element.SetValue(VerticalOffsetProperty, value);
        }

        public static double GetVerticalOffset(DependencyObject element)
        {
            return (double)element.GetValue(VerticalOffsetProperty);
        }


        public static readonly DependencyProperty ScrollableHeightProperty = DependencyProperty.RegisterAttached(
            "ScrollableHeight", typeof (double), typeof (ScrollViewer), new PropertyMetadata(default(double)));

        public static void SetScrollableHeight(DependencyObject element, double value)
        {
            element.SetValue(ScrollableHeightProperty, value);
        }

        public static double GetScrollableHeight(DependencyObject element)
        {
            return (double) element.GetValue(ScrollableHeightProperty);
        }

        #endregion

        private static bool? GetMouseWheelDirection(int delta)
        {
            if (delta > 0)
                return false;
            if (delta < 0)
                return true;
            return null;
        }

        private class ScrollViewerContext
        {
            public ScrollViewerContext(System.Windows.Controls.ScrollViewer scrollViewer, DependencyObject source)
            {
                if (scrollViewer == null) throw new ArgumentNullException(nameof(scrollViewer));
                if (source == null) throw new ArgumentNullException(nameof(source));

                ScrollViewer = scrollViewer;
                Source = source;
            }

            private EventHandler<MouseWheelEventArgs> _previewMouseWheelInternal;
            private EventHandler<ScrollChangedEventArgs> _scrollChangedInternal;

            public System.Windows.Controls.ScrollViewer ScrollViewer { get; }

            public DependencyObject Source { get; }

            private void OnPreviewMouseWheel(MouseWheelEventArgs e)
            {
                _previewMouseWheelInternal?.Invoke(this, e);
            }

            private void OnScrollChanged(ScrollChangedEventArgs e)
            {
                _scrollChangedInternal?.Invoke(this, e);
            }


            public event EventHandler<MouseWheelEventArgs> PreviewMouseWheel
            {
                add
                {
                    _previewMouseWheelInternal += value;
                    ScrollViewer.PreviewMouseWheel += ScrollViewerOnPreviewMouseWheel;
                }
                remove
                {
                    _previewMouseWheelInternal -= value;
                    ScrollViewer.PreviewMouseWheel -= ScrollViewerOnPreviewMouseWheel;
                }
            }

            public event EventHandler<ScrollChangedEventArgs> ScrollChanged
            {
                add
                {
                    _scrollChangedInternal += value;
                    ScrollViewer.ScrollChanged += ScrollViewerOnScrollChanged;
                }
                remove
                {
                    _scrollChangedInternal -= value;
                    ScrollViewer.ScrollChanged -= ScrollViewerOnScrollChanged;
                }
            }

            private void ScrollViewerOnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
            {
                OnPreviewMouseWheel(e);
            }

            private void ScrollViewerOnScrollChanged(object sender, ScrollChangedEventArgs e)
            {
                OnScrollChanged(e);
            }
        }
    }
}
