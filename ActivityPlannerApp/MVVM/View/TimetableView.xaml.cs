using ActivityPlannerApp.MVVM.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ActivityPlannerApp.MVVM.View
{
    /// <summary>
    /// Interaction logic for TimetableView.xaml
    /// </summary>
    public partial class TimetableView : UserControl
    {
        public TimetableView()
        {
            InitializeComponent();
            DataContextChanged += TimetableView_DataContextChanged;
        }

        private void TimetableView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is TimetableViewModel timetableViewModel)
            {
                AddColumns(timetableViewModel.DayColumns);
            }
        }

        private void AddColumns(IList<DateOnly> days)
        {
            // Add a column for each day, with each cell containing the TimetableCell.Content string
            for (int i = 0; i < days.Count; i++)
            {
                DateOnly day = days[i];
                DataGridTextColumn column = new DataGridTextColumn
                {
                    Header = day.ToString("ddd dd/MM/yy"),
                    Binding = new Binding("Content"),
                    Width = new DataGridLength(1, DataGridLengthUnitType.Star)
                };

                timetableDataGrid.Columns.Add(column);
            }
        }
    }
}
