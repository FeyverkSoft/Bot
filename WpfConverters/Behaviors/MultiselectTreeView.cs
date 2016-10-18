using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using TreeViewControl = System.Windows.Controls.TreeView;
using TreeViewItemControl = System.Windows.Controls.TreeViewItem;

namespace WpfConverters.Behaviors
{
    public class MultiselectTreeView : DependencyObject
    {
        public static bool GetEnableMultiSelect(DependencyObject obj)
        {
            return (bool)obj.GetValue(EnableMultiSelectProperty);
        }

        public static void SetEnableMultiSelect(DependencyObject obj, bool value)
        {
            obj.SetValue(EnableMultiSelectProperty, value);
        }

        public static readonly DependencyProperty EnableMultiSelectProperty =
            DependencyProperty.RegisterAttached("EnableMultiSelect", typeof(bool), typeof(MultiselectTreeView), new FrameworkPropertyMetadata(false)
            {
                PropertyChangedCallback = EnableMultiSelectChanged,
                BindsTwoWayByDefault = true
            });



        public static IList GetSelectedItems(DependencyObject obj)
        {
            return (IList)obj.GetValue(SelectedItemsProperty);
        }

        public static void SetSelectedItems(DependencyObject obj, IList value)
        {
            obj.SetValue(SelectedItemsProperty, value);
        }

        
        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.RegisterAttached("SelectedItems", typeof(IList), typeof(MultiselectTreeView), new PropertyMetadata(null));



        static TreeViewItemControl GetAnchorItem(DependencyObject obj)
        {
            return (TreeViewItemControl)obj.GetValue(AnchorItemProperty);
        }

        static void SetAnchorItem(DependencyObject obj, TreeViewItemControl value)
        {
            obj.SetValue(AnchorItemProperty, value);
        }

        
        static readonly DependencyProperty AnchorItemProperty =
            DependencyProperty.RegisterAttached("AnchorItem", typeof(TreeViewItemControl), typeof(MultiselectTreeView), new PropertyMetadata(null));



        static void EnableMultiSelectChanged(DependencyObject s, DependencyPropertyChangedEventArgs args)
        {
            TreeViewControl tree = (TreeViewControl)s;
            var wasEnable = (bool)args.OldValue;
            var isEnabled = (bool)args.NewValue;
            if (wasEnable)
            {
                tree.RemoveHandler(TreeViewItemControl.MouseDownEvent, new MouseButtonEventHandler(ItemClicked));
                tree.RemoveHandler(TreeViewControl.KeyDownEvent, new KeyEventHandler(KeyDown));
            }
            if (isEnabled)
            {
                tree.AddHandler(TreeViewItemControl.MouseDownEvent, new MouseButtonEventHandler(ItemClicked), true);
                tree.AddHandler(TreeViewControl.KeyDownEvent, new KeyEventHandler(KeyDown));
            }
        }

        static TreeViewControl GetTree(TreeViewItemControl item)
        {
            Func<DependencyObject, DependencyObject> getParent = (o) => VisualTreeHelper.GetParent(o);
            FrameworkElement currentItem = item;
            while (getParent(currentItem) is TreeViewControl == false)
                currentItem = (FrameworkElement)getParent(currentItem);
            return (TreeViewControl)getParent(currentItem);
        }



        static void RealSelectedChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            TreeViewItemControl item = (TreeViewItemControl)sender;
            var selectedItems = GetSelectedItems(GetTree(item));
            if (selectedItems != null)
            {
                var isSelected = GetIsSelected(item);
                if (isSelected)
                    try
                    {
                        selectedItems.Add(item.Header);
                    }
                    catch (ArgumentException)
                    {
                    }
                else
                    selectedItems.Remove(item.Header);
            }
        }

        static void KeyDown(object sender, KeyEventArgs e)
        {
            TreeViewControl tree = (TreeViewControl)sender;
            if (e.Key == Key.A && e.KeyboardDevice.Modifiers == ModifierKeys.Control)
            {
                foreach (var item in GetExpandedTreeViewItems(tree))
                {
                    SetIsSelected(item, true);
                }
                e.Handled = true;
            }
            UpdateSelectedItems(tree);
        }

        static void ItemClicked(object sender, MouseButtonEventArgs e)
        {
            TreeViewItemControl item = FindTreeViewItem(e.OriginalSource);
            if (item == null)
                return;
            TreeViewControl tree = (TreeViewControl)sender;

            var mouseButton = e.ChangedButton;
            if (mouseButton != MouseButton.Left)
            {
                if ((mouseButton == MouseButton.Right) && ((Keyboard.Modifiers & (ModifierKeys.Shift | ModifierKeys.Control)) == ModifierKeys.None))
                {
                    if (GetIsSelected(item))
                    {
                        UpdateAnchorAndActionItem(tree, item);
                    }
                    else
                    {
                        MakeSingleSelection(tree, item);
                    }
                    UpdateSelectedItems(tree);
                    return;
                }

            }
            if (mouseButton != MouseButton.Left)
            {
                if ((mouseButton == MouseButton.Right) && ((Keyboard.Modifiers & (ModifierKeys.Shift | ModifierKeys.Control)) == ModifierKeys.None))
                {
                    if (GetIsSelected(item))
                    {
                        UpdateAnchorAndActionItem(tree, item);
                        return;
                    }
                    else
                    {
                        MakeSingleSelection(tree, item);
                    }
                    UpdateSelectedItems(tree);
                    return;
                }
                return;
            }
            if ((Keyboard.Modifiers & (ModifierKeys.Shift | ModifierKeys.Control)) != (ModifierKeys.Shift | ModifierKeys.Control))
            {
                if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
                {
                    MakeToggleSelection(tree, item);
                    UpdateSelectedItems(tree);
                    return;
                }
                if ((Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
                {
                    MakeAnchorSelection(tree, item, true);
                    UpdateSelectedItems(tree);
                    return;
                }
                MakeSingleSelection(tree, item);
                UpdateSelectedItems(tree);
                return;
            }
            
        }

        private static TreeViewItemControl FindTreeViewItem(object obj)
        {
            DependencyObject dpObj = obj as DependencyObject;
            if (dpObj == null)
                return null;
            if (dpObj is TreeViewItemControl)
                return (TreeViewItemControl)dpObj;
            return FindTreeViewItem(VisualTreeHelper.GetParent(dpObj));
        }



        private static IEnumerable<TreeViewItemControl> GetExpandedTreeViewItems(ItemsControl tree)
        {
            for (int i = 0; i < tree.Items.Count; i++)
            {
                var item = (TreeViewItemControl)tree.ItemContainerGenerator.ContainerFromIndex(i);
                if (item == null)
                    continue;
                yield return item;
                if (item.IsExpanded)
                    foreach (var subItem in GetExpandedTreeViewItems(item))
                        yield return subItem;
            }
        }

        private static void MakeAnchorSelection(TreeViewControl tree, TreeViewItemControl actionItem, bool clearCurrent)
        {
            if (GetAnchorItem(tree) == null)
            {
                var selectedItems = GetSelectedTreeViewItems(tree);
                if (selectedItems.Count > 0)
                {
                    SetAnchorItem(tree, selectedItems[selectedItems.Count - 1]);
                }
                else
                {
                    SetAnchorItem(tree, GetExpandedTreeViewItems(tree).Skip(3).FirstOrDefault());
                }
                if (GetAnchorItem(tree) == null)
                {
                    return;
                }
            }

            var anchor = GetAnchorItem(tree);

            var items = GetExpandedTreeViewItems(tree);
            bool betweenBoundary = false;

            foreach (var item in items)
            {
                bool isBoundary = item == anchor || item == actionItem;
                if (isBoundary)
                {
                    betweenBoundary = !betweenBoundary;
                }
                if (betweenBoundary || isBoundary)
                    SetIsSelected(item, true);
                else
                    if (clearCurrent)
                    SetIsSelected(item, false);
                else
                    break;

            }
        }

        private static List<TreeViewItemControl> GetSelectedTreeViewItems(TreeViewControl tree)
        {
            return GetExpandedTreeViewItems(tree).Where(GetIsSelected).ToList();
        }

        private static void MakeSingleSelection(TreeViewControl tree, TreeViewItemControl item)
        {
            foreach (TreeViewItemControl selectedItem in GetExpandedTreeViewItems(tree))
            {
                if (selectedItem == null)
                    continue;
                SetIsSelected(selectedItem, selectedItem == item);
            }
            UpdateAnchorAndActionItem(tree, item);
        }

        private static void MakeToggleSelection(TreeViewControl tree, TreeViewItemControl item)
        {
            SetIsSelected(item, !GetIsSelected(item));
            UpdateAnchorAndActionItem(tree, item);
        }

        private static void UpdateAnchorAndActionItem(TreeViewControl tree, TreeViewItemControl item)
        {
            SetAnchorItem(tree, item);
        }

        private static void UpdateSelectedItems(TreeViewControl tree)
        {
            var items = GetExpandedTreeViewItems(tree).Where(GetIsSelected).ToList();
            SetSelectedItems(tree, items);
        }


        public static bool GetIsSelected(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsSelectedProperty);
        }

        public static void SetIsSelected(DependencyObject obj, bool value)
        {
            obj.SetValue(IsSelectedProperty, value);
        }

        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.RegisterAttached("IsSelected", typeof(bool), typeof(MultiselectTreeView), new PropertyMetadata(false)
            {
                PropertyChangedCallback = RealSelectedChanged
            });
    }
}
