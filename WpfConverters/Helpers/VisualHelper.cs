using System;
using System.Windows;
using System.Windows.Media;

namespace WpfConverters.Helpers
{
   static class VisualHelper
    {
        public static DependencyObject FindVisualChild(DependencyObject obj, Type childType)
        {
            if (typeof(DependencyObject).IsAssignableFrom(childType) == false)
                return null;

            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                var child = VisualTreeHelper.GetChild(obj, i);

                var type = child.GetType();
                if (childType.IsAssignableFrom(type))
                    return child;

                var childOfChild = FindVisualChild(child, childType);

                if (childOfChild != null)
                    return childOfChild;
            }

            return null;
        }

        public static T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                var child = VisualTreeHelper.GetChild(obj, i);

                var item = child as T;
                if (item != null)
                    return item;

                var childOfChild = FindVisualChild<T>(child);

                if (childOfChild != null)
                    return childOfChild;
            }

            return null;
        }

        public static T FindVisualChild<T>(DependencyObject obj, Func<T, bool> predicate) where T : DependencyObject
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                var child = VisualTreeHelper.GetChild(obj, i);

                var item = child as T;
                if (item != null && predicate(item))
                    return item;

                var childOfChild = FindVisualChild(child, predicate);

                if (childOfChild != null)
                    return childOfChild;
            }

            return null;
        }

        public static T FindParent<T>(DependencyObject obj) where T : DependencyObject
        {
            if (obj == null)
                return null;

            var parent = VisualTreeHelper.GetParent(obj);
            if (parent == null)
                return null;

            var parentType = parent as T;
            return parentType ?? FindParent<T>(parent);
        }
    }
}
