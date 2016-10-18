using System.Windows;
using System.Windows.Input;

namespace WpfConverters.Behaviors
{
    public static class DragAndDrop
    {
        public static readonly DependencyProperty AttachDragAndDropProperty = DependencyProperty.RegisterAttached(
            "AttachDragAndDrop", typeof(bool), typeof(DragAndDrop), new PropertyMetadata(default(bool), AttachDragAndDropChangedCallback));

        private static void AttachDragAndDropChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var element = dependencyObject as FrameworkElement;
            if (element == null)
                return;

            var value = (bool)e.NewValue;
            if (value == true)
            {
                element.PreviewDragEnter += ElementOnPreviewDragEnter;
                element.PreviewDragLeave += ElementOnPreviewDragLeave;
                element.PreviewDrop += ElementOnPreviewDrop;
                element.Drop += ElementOnDrop;
            }
            else
            {
                element.PreviewDragEnter -= ElementOnPreviewDragEnter;
                element.PreviewDragLeave -= ElementOnPreviewDragLeave;
                element.PreviewDrop -= ElementOnPreviewDrop;
            }
        }

        private static void ElementOnPreviewDragEnter(object sender, DragEventArgs e)
        {
            var element = (FrameworkElement)sender;
            SetIsDragEnter(element, true);
        }

        private static void ElementOnPreviewDragLeave(object sender, DragEventArgs e)
        {
            var element = (FrameworkElement)sender;
            SetIsDragEnter(element, false);
        }

        private static void ElementOnPreviewDrop(object sender, DragEventArgs e)
        {
            var element = (FrameworkElement)sender;
            SetIsDragEnter(element, false);
        }

        private static void ElementOnDrop(object sender, DragEventArgs e)
        {
            var element = (FrameworkElement)sender;
            var command = GetDropCommand(element);

            if (command == null)
                return;

            if (command.CanExecute(e.Data))
                command.Execute(e.Data);
        }

        public static void SetAttachDragAndDrop(DependencyObject element, bool value)
        {
            element.SetValue(AttachDragAndDropProperty, value);
        }

        public static bool GetAttachDragAndDrop(DependencyObject element)
        {
            return (bool)element.GetValue(AttachDragAndDropProperty);
        }


        public static readonly DependencyProperty IsDragEnterProperty = DependencyProperty.RegisterAttached(
            "IsDragEnter", typeof(bool), typeof(DragAndDrop), new PropertyMetadata(default(bool)));

        private static void SetIsDragEnter(DependencyObject element, bool value)
        {
            element.SetValue(IsDragEnterProperty, value);
        }

        public static bool GetIsDragEnter(DependencyObject element)
        {
            return (bool)element.GetValue(IsDragEnterProperty);
        }


        public static readonly DependencyProperty DropCommandProperty = DependencyProperty.RegisterAttached(
            "DropCommand", typeof(ICommand), typeof(DragAndDrop), new PropertyMetadata(default(ICommand)));

        public static void SetDropCommand(DependencyObject element, ICommand value)
        {
            element.SetValue(DropCommandProperty, value);
        }

        public static ICommand GetDropCommand(DependencyObject element)
        {
            return (ICommand)element.GetValue(DropCommandProperty);
        }
    }
}
