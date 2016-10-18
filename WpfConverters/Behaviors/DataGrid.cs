using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfConverters.Behaviors
{
    public static class DataGrid
    {
        #region Display row number

        public static readonly DependencyProperty DisplayRowNumberProperty = DependencyProperty.RegisterAttached(
            "DisplayRowNumber", typeof(bool), typeof(DataGrid),
            new PropertyMetadata(default(bool), DisplayRowNumberChangedCallback));

        private static void DisplayRowNumberChangedCallback(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs e)
        {
            var dataGrid = dependencyObject as System.Windows.Controls.DataGrid;
            if (dataGrid == null)
                return;

            var diplay = e.NewValue as bool? ?? false;
            if (diplay)
                dataGrid.LoadingRow += DataGridOnLoadingRow;
            else
                dataGrid.LoadingRow -= DataGridOnLoadingRow;
        }

        private static void DataGridOnLoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        public static void SetDisplayRowNumber(DependencyObject element, bool value)
        {
            element.SetValue(DisplayRowNumberProperty, value);
        }

        public static bool GetDisplayRowNumber(DependencyObject element)
        {
            return (bool)element.GetValue(DisplayRowNumberProperty);
        }

        #endregion

        #region Display row header

        public static readonly DependencyProperty RowHeadersProperty = DependencyProperty.RegisterAttached(
            "RowHeaders", typeof(object[]), typeof(DataGrid), new PropertyMetadata(default(object[])));

        public static void SetRowHeaders(DependencyObject element, object[] value)
        {
            element.SetValue(RowHeadersProperty, value);
        }

        public static object[] GetRowHeaders(DependencyObject element)
        {
            return (object[])element.GetValue(RowHeadersProperty);
        }


        public static readonly DependencyProperty DisplayRowHeaderProperty = DependencyProperty.RegisterAttached(
            "DisplayRowHeader", typeof(bool), typeof(DataGrid), new PropertyMetadata(default(bool), DisplayRowHeaderChangedCallback));

        private static void DisplayRowHeaderChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var dataGrid = dependencyObject as System.Windows.Controls.DataGrid;
            if (dataGrid == null)
                return;

            var display = e.NewValue as bool? ?? false;
            if (display)
                dataGrid.LoadingRow += DataGridOnLoadingRow2;
            else
                dataGrid.LoadingRow -= DataGridOnLoadingRow2;
        }

        private static void DataGridOnLoadingRow2(object sender, DataGridRowEventArgs e)
        {
            var target = sender as DependencyObject;
            if (target == null)
                return;

            var rowHeaders = GetRowHeaders(target);
            if (rowHeaders?.Any() != true)
                return;

            var index = e.Row.GetIndex();
            if (index >= rowHeaders.Length)
                return;

            var rowHeader = rowHeaders[index];
            e.Row.Header = rowHeader;
        }

        public static void SetDisplayRowHeader(DependencyObject element, bool value)
        {
            element.SetValue(DisplayRowHeaderProperty, value);
        }

        public static bool GetDisplayRowHeader(DependencyObject element)
        {
            return (bool)element.GetValue(DisplayRowHeaderProperty);
        }

        #endregion

        #region Selected values

        private static IEnumerable<object> GetSelectedCellsValues(IList<DataGridCellInfo> cells)
        {
            return from cell in cells
                   let rowView = cell.Item as DataRowView
                   where rowView != null
                   let columnName = cell.Column.Header as string
                   where columnName != null
                   where rowView.DataView.Table.Columns.Contains(columnName)
                   select rowView[columnName];
        }

        private static decimal? GetValuesSum(IList<object> items)
        {
            if (items == null || !items.Any())
                return null;

            var values = from v in items
                         where v != null && (v is decimal || v is double)
                         select Convert.ToDecimal(v);

            try
            {
                return values.Sum();
            }
            catch
            {
                return null;
            }
        }

        private static IReadOnlyCollection<string> GetSelectedColumns(IEnumerable<string> oldSelectedColumns, IEnumerable<DataGridCellInfo> selectedCells)
        {
            var selectedColumns =
                selectedCells.Select(x => x.Column)
                    .Where(x => x != null)
                    .Distinct()
                    .Select(x => x.Header as string)
                    .Where(x => x != null)
                    .ToList();

            var currentColumns = new List<string>(oldSelectedColumns ?? Enumerable.Empty<string>());

            var removedColumns = currentColumns.Where(x => !selectedColumns.Contains(x)).ToList();
            foreach (var removedColumn in removedColumns)
                currentColumns.Remove(removedColumn);

            var addedColumns = selectedColumns.Where(x => !currentColumns.Contains(x)).ToList();
            currentColumns.AddRange(addedColumns);

            return currentColumns;
        }

        public static readonly DependencyProperty TrackSelectionChangesProperty = DependencyProperty.RegisterAttached(
            "TrackSelectionChanges", typeof(bool), typeof(DataGrid), new PropertyMetadata(default(bool), TrackSelectionChangesChangedCallback));

        private static void TrackSelectionChangesChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var dataGrid = dependencyObject as System.Windows.Controls.DataGrid;
            if (dataGrid == null)
                return;

            dataGrid.SelectedCellsChanged -= DataGridOnSelectedCellsChanged;

            var track = e.NewValue as bool? ?? false;
            if (track)
                dataGrid.SelectedCellsChanged += DataGridOnSelectedCellsChanged;
        }

        private static void DataGridOnSelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            var dataGrid = sender as System.Windows.Controls.DataGrid;
            if (dataGrid == null)
                return;

            var cells = dataGrid.SelectedCells;

            var values = GetSelectedCellsValues(cells).ToList();
            SetSelectedValues(dataGrid, values);

            var selectedRows = cells.Select(x => x.Item).OfType<DataRowView>().Distinct().ToArray();
            SetSelectedRows(dataGrid, selectedRows);
            SetSelectedRow(dataGrid, selectedRows.FirstOrDefault());

            var selectedColumns = GetSelectedColumns(dataGrid);
            selectedColumns = GetSelectedColumns(selectedColumns, cells);
            SetSelectedColumns(dataGrid, selectedColumns);

            var calculate = GetCalculateValues(dataGrid);
            if (calculate == true)
            {
                var selectedValuesSum = GetValuesSum(values);
                SetSelectedValuesSum(dataGrid, selectedValuesSum);

                decimal? selectedValuesAvg = null;
                if (selectedValuesSum != null && values.Count > 0)
                    selectedValuesAvg = selectedValuesSum / values.Count;
                SetSelectedValuesAvg(dataGrid, selectedValuesAvg);

                var dataView = dataGrid.ItemsSource as DataView;
                if (dataView != null)
                {
                    var columns = selectedColumns.Select(x => x).Where(x => x != null);
                    var columnsValues =
                        columns.SelectMany(
                            columnName =>
                                from DataRowView row in dataView
                                where row.DataView.Table.Columns.Contains(columnName)
                                select row[columnName]).ToList();
                    var columnsSum = GetValuesSum(columnsValues);
                    SetSelectedColumnsSum(dataGrid, columnsSum);
                }
            }
        }

        public static void SetTrackSelectionChanges(DependencyObject element, bool value)
        {
            element.SetValue(TrackSelectionChangesProperty, value);
        }

        public static bool GetTrackSelectionChanges(DependencyObject element)
        {
            return (bool)element.GetValue(TrackSelectionChangesProperty);
        }


        public static readonly DependencyProperty SelectedValuesProperty = DependencyProperty.RegisterAttached(
            "SelectedValues", typeof(IReadOnlyCollection<object>), typeof(DataGrid), new PropertyMetadata(default(IReadOnlyCollection<object>)));

        public static void SetSelectedValues(DependencyObject element, IReadOnlyCollection<object> value)
        {
            element.SetValue(SelectedValuesProperty, value);
        }

        public static IReadOnlyCollection<object> GetSelectedValues(DependencyObject element)
        {
            return (IReadOnlyCollection<object>)element.GetValue(SelectedValuesProperty);
        }


        public static readonly DependencyProperty SelectedRowsProperty = DependencyProperty.RegisterAttached(
            "SelectedRows", typeof(IReadOnlyCollection<DataRowView>), typeof(DataGrid), new PropertyMetadata(default(IReadOnlyCollection<DataRowView>)));

        public static void SetSelectedRows(DependencyObject element, IReadOnlyCollection<DataRowView> value)
        {
            element.SetValue(SelectedRowsProperty, value);
        }

        public static IReadOnlyCollection<DataRowView> GetSelectedRows(DependencyObject element)
        {
            return (IReadOnlyCollection<DataRowView>)element.GetValue(SelectedRowsProperty);
        }


        public static readonly DependencyProperty SelectedRowProperty = DependencyProperty.RegisterAttached(
            "SelectedRow", typeof(DataRowView), typeof(DataGrid), new PropertyMetadata(default(DataRowView)));

        public static void SetSelectedRow(DependencyObject element, DataRowView value)
        {
            element.SetValue(SelectedRowProperty, value);
        }

        public static DataRowView GetSelectedRow(DependencyObject element)
        {
            return (DataRowView)element.GetValue(SelectedRowProperty);
        }


        public static readonly DependencyProperty SelectedColumnsProperty = DependencyProperty.RegisterAttached(
            "SelectedColumns", typeof(IReadOnlyCollection<string>), typeof(DataGrid), new PropertyMetadata(default(IReadOnlyCollection<string>)));

        public static void SetSelectedColumns(DependencyObject element, IReadOnlyCollection<string> value)
        {
            element.SetValue(SelectedColumnsProperty, value);
        }

        public static IReadOnlyCollection<string> GetSelectedColumns(DependencyObject element)
        {
            return (IReadOnlyCollection<string>)element.GetValue(SelectedColumnsProperty);
        }


        public static readonly DependencyProperty CalculateValuesProperty = DependencyProperty.RegisterAttached(
            "CalculateValues", typeof(bool), typeof(DataGrid), new PropertyMetadata(default(bool)));

        public static void SetCalculateValues(DependencyObject element, bool value)
        {
            element.SetValue(CalculateValuesProperty, value);
        }

        public static bool GetCalculateValues(DependencyObject element)
        {
            return (bool)element.GetValue(CalculateValuesProperty);
        }


        public static readonly DependencyProperty SelectedValuesSumProperty = DependencyProperty.RegisterAttached(
            "SelectedValuesSum", typeof(decimal?), typeof(DataGrid), new PropertyMetadata(default(decimal?)));

        public static void SetSelectedValuesSum(DependencyObject element, decimal? value)
        {
            element.SetValue(SelectedValuesSumProperty, value);
        }

        public static decimal? GetSelectedValuesSum(DependencyObject element)
        {
            return (decimal?)element.GetValue(SelectedValuesSumProperty);
        }


        public static readonly DependencyProperty SelectedValuesAvgProperty = DependencyProperty.RegisterAttached(
            "SelectedValuesAvg", typeof(decimal?), typeof(DataGrid), new PropertyMetadata(default(decimal?)));

        public static void SetSelectedValuesAvg(DependencyObject element, decimal? value)
        {
            element.SetValue(SelectedValuesAvgProperty, value);
        }

        public static decimal? GetSelectedValuesAvg(DependencyObject element)
        {
            return (decimal?)element.GetValue(SelectedValuesAvgProperty);
        }


        public static readonly DependencyProperty SelectedColumnsSumProperty = DependencyProperty.RegisterAttached(
            "SelectedColumnsSum", typeof(decimal?), typeof(DataGrid), new PropertyMetadata(default(decimal?)));

        public static void SetSelectedColumnsSum(DependencyObject element, decimal? value)
        {
            element.SetValue(SelectedColumnsSumProperty, value);
        }

        public static decimal? GetSelectedColumnsSum(DependencyObject element)
        {
            return (decimal?)element.GetValue(SelectedColumnsSumProperty);
        }

        #endregion

        #region Column drag and drop

        public static readonly DependencyProperty ReorderColumnCommandProperty = DependencyProperty.RegisterAttached(
            "ReorderColumnCommand", typeof(ICommand), typeof(DataGrid), new PropertyMetadata(default(ICommand), MoveColumnCommandChangedCallback));

        private static void MoveColumnCommandChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var dataGrid = dependencyObject as System.Windows.Controls.DataGrid;
            if (dataGrid == null)
                return;

            dataGrid.ColumnReordered -= DataGridOnColumnReordered;

            var command = e.NewValue as ICommand;
            if (command != null)
            {
                dataGrid.ColumnReordered += DataGridOnColumnReordered;
            }
        }

        private static void DataGridOnColumnReordered(object sender, DataGridColumnEventArgs e)
        {
            var dataGrid = (System.Windows.Controls.DataGrid)sender;

            var command = GetReorderColumnCommand(dataGrid);
            if (command == null)
                return;

            var parameter = new ReorderColumnInfo((string)e.Column.Header, e.Column.DisplayIndex);
            if (command.CanExecute(parameter) == true)
            {
                command.Execute(parameter);
            }
        }

        public static void SetReorderColumnCommand(DependencyObject element, ICommand value)
        {
            element.SetValue(ReorderColumnCommandProperty, value);
        }

        public static ICommand GetReorderColumnCommand(DependencyObject element)
        {
            return (ICommand)element.GetValue(ReorderColumnCommandProperty);
        }

        #endregion
    }


    public class ReorderColumnInfo
    {
        public ReorderColumnInfo(string columnName, int index)
        {
            ColumnName = columnName;
            Index = index;
        }

        public string ColumnName { get; }

        public int Index { get; }
    }
}
