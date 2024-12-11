using ActivityPlannerApp.MVVM.ViewModel;
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

            TimetableViewModel viewModel = new TimetableViewModel();
            DataContext = viewModel;

            AddColumns(viewModel.Days);
        }

        private void AddColumns(List<DateOnly> days)
        {
            for (int i = 0; i < days.Count; i++)
            {
                DateOnly day = days[i];
                DataGridTextColumn column = new DataGridTextColumn
                {
                    Header = day.ToString("ddd dd/MM/yy"),

                    // Cells in this column should show the activity string for that day at each time range
                    Binding = new Binding("ActivityString"),

                    Width = new DataGridLength(1, DataGridLengthUnitType.Star)
                };

                timetableDataGrid.Columns.Add(column);
            }
        }
    }
}
