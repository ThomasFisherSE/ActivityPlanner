using ActivityPlannerApp.MVVM.Model;
using ActivityPlannerApp.MVVM.ViewModel;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace ActivityPlannerApp.MVVM.View
{
    /// <summary>
    /// Interaction logic for TimetableView.xaml
    /// </summary>
    public partial class TimetableView : UserControl
    {
        private const int RowHeadersColumnIdx = 0;
        private const int ColumnHeadersRowIdx = 0;
        private const int DefaultRowHeightPx = 50;
        private const string HeaderTextBlockStyleKey = "HeaderTextBlockStyle";
        private const string TimetableEntryTextBlockStyleKey = "TimetableEntryTextBlockStyle";
        private const string GridCellBorderStyleKey = "GridCellBorderStyle";

        private readonly GridLength _defaultRowHeight = new(DefaultRowHeightPx);

        public TimetableView()
        {
            InitializeComponent();
            DataContextChanged += TimetableView_DataContextChanged;
        }

        private void TimetableView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is TimetableViewModel timetableViewModel)
            {
                InitializeTimetable(timetableViewModel);
                AddTimetableEntries(timetableViewModel.TimetableEntryGridInfoCollection);

                timetableViewModel.TimetableEntryGridInfoCollection.CollectionChanged += TimetableEntryGridInfoCollection_CollectionChanged;
            }
        }

        private void TimetableEntryGridInfoCollection_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    System.Collections.IList? newItems = e.NewItems;
                    if (newItems != null)
                    {
                        AddTimetableEntries(newItems.Cast<TimetableEntryGridInfo>().ToList());
                    }
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
                case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                    // Ignore for now, could handle these types of collection change in the future
                    break;
            }
        }

        private void AddTimetableEntries(IList<TimetableEntryGridInfo> timetableEntryGridInfoItems)
        {
            foreach(TimetableEntryGridInfo timetableEntryGridInfo in timetableEntryGridInfoItems)
            {
                AddBorderedTextBlock(timetableEntryGridInfo.TimetableEntry.Content, 
                    timetableEntryGridInfo.Row, 
                    timetableEntryGridInfo.Column,
                    TimetableEntryTextBlockStyleKey,
                    timetableEntryGridInfo.RowSpan);
            }
        }

        private void InitializeTimetable(TimetableViewModel timetableViewModel)
        {
            ColumnDefinitionCollection columnDefinitions = timetableGrid.ColumnDefinitions;
            RowDefinitionCollection rowDefinitions = timetableGrid.RowDefinitions;
            columnDefinitions.Clear();
            rowDefinitions.Clear();

            // Add header row and column
            rowDefinitions.Add(new RowDefinition() { Height = _defaultRowHeight });
            columnDefinitions.Add(new ColumnDefinition());

            // Days
            InitializeDayColumns(columnDefinitions, timetableViewModel.ColumnDays);

            // Time range rows
            InitializeTimeRows(rowDefinitions, timetableViewModel.RowTimeRanges);
        }

        private void InitializeDayColumns(ColumnDefinitionCollection columnDefinitions, IList<DateOnly> columnDays)
        {
            for (int dayIdx = 0; dayIdx < columnDays.Count; dayIdx++)
            {
                DateOnly day = columnDays[dayIdx];

                int colIdx = columnDefinitions.Count;
                columnDefinitions.Add(new ColumnDefinition());

                // Set header text
                string dayHeaderText = day.ToString("ddd dd/MM/yy");
                AddBorderedTextBlock(dayHeaderText, ColumnHeadersRowIdx, colIdx, HeaderTextBlockStyleKey);
            }
        }

        private void InitializeTimeRows(RowDefinitionCollection rowDefinitions, IList<TimeRange> rowTimeRanges)
        {
            AddBorderedTextBlock("Time", ColumnHeadersRowIdx, RowHeadersColumnIdx, HeaderTextBlockStyleKey);

            for (int timeRangeIdx = 0; timeRangeIdx < rowTimeRanges.Count; timeRangeIdx++)
            {
                TimeRange timeRange = rowTimeRanges[timeRangeIdx];

                int rowIdx = rowDefinitions.Count;
                rowDefinitions.Add(new RowDefinition() { Height = _defaultRowHeight });

                // Set header text
                string timeHeaderText = timeRange.ToString();
                AddBorderedTextBlock(timeHeaderText, rowIdx, RowHeadersColumnIdx, HeaderTextBlockStyleKey);
            }
        }

        private void AddBorderedTextBlock(string text, int row, int column, string? styleKey = null, int GridRowSpan = 1)
        {
            Border gridCellBorder = new()
            {
                Style = (Style)FindResource(GridCellBorderStyleKey)
            };

            TextBlock textBlock = new()
            { 
                Text = text
            };

            if (styleKey != null)
            {
                textBlock.Style = (Style)FindResource(styleKey);
            }

            gridCellBorder.Child = textBlock;

            Grid.SetRow(gridCellBorder, row);
            Grid.SetColumn(gridCellBorder, column);

            if (GridRowSpan > 1)
            { 
                Grid.SetRowSpan(gridCellBorder, GridRowSpan); 
            }

            timetableGrid.Children.Add(gridCellBorder); 
        }
    }
}
