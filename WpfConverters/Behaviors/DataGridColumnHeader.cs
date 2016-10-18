using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfConverters.Behaviors
{
    public static class DataGridColumnHeader
    {
        public static readonly DependencyProperty IsFilteredProperty = DependencyProperty.RegisterAttached(
            "IsFiltered", typeof(bool), typeof(DataGridColumnHeader), new PropertyMetadata(default(bool)));

        public static void SetIsFiltered(DependencyObject element, bool value)
        {
            element.SetValue(IsFilteredProperty, value);
        }

        public static bool GetIsFiltered(DependencyObject element)
        {
            return (bool)element.GetValue(IsFilteredProperty);
        }


        public static readonly DependencyProperty TitleXmlProperty = DependencyProperty.RegisterAttached(
            "TitleXml", typeof(string), typeof(DataGridColumnHeader), new PropertyMetadata(default(string)));

        public static void SetTitleXml(DependencyObject element, string value)
        {
            element.SetValue(TitleXmlProperty, value);
        }

        public static string GetTitleXml(DependencyObject element)
        {
            return (string)element.GetValue(TitleXmlProperty);
        }


        public static readonly DependencyProperty DataTypeNameProperty = DependencyProperty.RegisterAttached(
            "DataTypeName", typeof(string), typeof(DataGridColumnHeader), new PropertyMetadata(default(string)));

        public static void SetDataTypeName(DependencyObject element, string value)
        {
            element.SetValue(DataTypeNameProperty, value);
        }

        public static string GetDataTypeName(DependencyObject element)
        {
            return (string)element.GetValue(DataTypeNameProperty);
        }


        public static readonly DependencyProperty IndexProperty = DependencyProperty.RegisterAttached(
            "Index", typeof(int?), typeof(DataGridColumnHeader), new PropertyMetadata(default(int?)));

        public static void SetIndex(DependencyObject element, int? value)
        {
            element.SetValue(IndexProperty, value);
        }

        public static int? GetIndex(DependencyObject element)
        {
            return (int?)element.GetValue(IndexProperty);
        }


        public static readonly DependencyProperty IsIndexVisibleProperty = DependencyProperty.RegisterAttached(
            "IsIndexVisible", typeof(bool), typeof(DataGridColumnHeader), new PropertyMetadata(false));

        public static void SetIsIndexVisible(DependencyObject element, bool value)
        {
            element.SetValue(IsIndexVisibleProperty, value);
        }

        public static bool GetIsIndexVisible(DependencyObject element)
        {
            return (bool)element.GetValue(IsIndexVisibleProperty);
        }


   


        public static readonly DependencyProperty IsValuesFilterProperty = DependencyProperty.RegisterAttached(
            "IsValuesFilter", typeof(bool), typeof(DataGridColumnHeader), new PropertyMetadata(default(bool)));

        public static void SetIsValuesFilter(DependencyObject element, bool value)
        {
            element.SetValue(IsValuesFilterProperty, value);
        }

        public static bool GetIsValuesFilter(DependencyObject element)
        {
            return (bool)element.GetValue(IsValuesFilterProperty);
        }


        public static readonly DependencyProperty FilterTemplateSelectorProperty = DependencyProperty.RegisterAttached(
            "FilterTemplateSelector", typeof(DataTemplateSelector), typeof(DataGridColumnHeader), new PropertyMetadata(default(DataTemplateSelector)));

        public static void SetFilterTemplateSelector(DependencyObject element, DataTemplateSelector value)
        {
            element.SetValue(FilterTemplateSelectorProperty, value);
        }

        public static DataTemplateSelector GetFilterTemplateSelector(DependencyObject element)
        {
            return (DataTemplateSelector)element.GetValue(FilterTemplateSelectorProperty);
        }

        public static readonly DependencyProperty BeforeApplyChangesCommandProperty = DependencyProperty.RegisterAttached(
            "BeforeApplyChangesCommand", typeof(ICommand), typeof(DataGridColumnHeader), new PropertyMetadata(default(ICommand)));

        public static void SetBeforeApplyChangesCommand(DependencyObject element, ICommand value)
        {
            element.SetValue(BeforeApplyChangesCommandProperty, value);
        }

        public static ICommand GetBeforeApplyChangesCommand(DependencyObject element)
        {
            return (ICommand)element.GetValue(BeforeApplyChangesCommandProperty);
        }


        public static readonly DependencyProperty ApplyChangesCommandProperty = DependencyProperty.RegisterAttached(
            "ApplyChangesCommand", typeof(ICommand), typeof(DataGridColumnHeader), new PropertyMetadata(default(ICommand)));

        public static void SetApplyChangesCommand(DependencyObject element, ICommand value)
        {
            element.SetValue(ApplyChangesCommandProperty, value);
        }

        public static ICommand GetApplyChangesCommand(DependencyObject element)
        {
            return (ICommand)element.GetValue(ApplyChangesCommandProperty);
        }


        public static readonly DependencyProperty CancelChangesCommandProperty = DependencyProperty.RegisterAttached(
            "CancelChangesCommand", typeof (ICommand), typeof (DataGridColumnHeader), new PropertyMetadata(default(ICommand)));

        public static void SetCancelChangesCommand(DependencyObject element, ICommand value)
        {
            element.SetValue(CancelChangesCommandProperty, value);
        }

        public static ICommand GetCancelChangesCommand(DependencyObject element)
        {
            return (ICommand) element.GetValue(CancelChangesCommandProperty);
        }


        public static readonly DependencyProperty ChangeSortDirectionCommandProperty = DependencyProperty.RegisterAttached(
            "ChangeSortDirectionCommand", typeof (ICommand), typeof (DataGridColumnHeader), new PropertyMetadata(default(ICommand)));

        public static void SetChangeSortDirectionCommand(DependencyObject element, ICommand value)
        {
            element.SetValue(ChangeSortDirectionCommandProperty, value);
        }

        public static ICommand GetChangeSortDirectionCommand(DependencyObject element)
        {
            return (ICommand) element.GetValue(ChangeSortDirectionCommandProperty);
        }
    }
}
