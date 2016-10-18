using System;
using System.Windows.Controls;

namespace WpfConverters.Extensions
{
    public static class ScrollViewerExtensions
    {
        public static void LineDown(this ScrollViewer scrollViewer, int count)
        {
            if (scrollViewer == null) throw new ArgumentNullException(nameof(scrollViewer));
            if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count));

            for (int i = 0; i < count; i++)
            {
                scrollViewer.LineDown();
            }
        }

        public static void LineUp(this ScrollViewer scrollViewer, int count)
        {
            if (scrollViewer == null) throw new ArgumentNullException(nameof(scrollViewer));
            if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count));

            for (int i = 0; i < count; i++)
            {
                scrollViewer.LineUp();
            }
        }

        public static void LineRight(this ScrollViewer scrollViewer, int count)
        {
            if (scrollViewer == null) throw new ArgumentNullException(nameof(scrollViewer));
            if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count));

            for (int i = 0; i < count; i++)
            {
                scrollViewer.LineRight();
            }
        }

        public static void LineLeft(this ScrollViewer scrollViewer, int count)
        {
            if (scrollViewer == null) throw new ArgumentNullException(nameof(scrollViewer));
            if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count));

            for (int i = 0; i < count; i++)
            {
                scrollViewer.LineLeft();
            }
        }
    }
}
