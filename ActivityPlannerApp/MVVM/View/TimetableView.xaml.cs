using ActivityPlannerApp.MVVM.ViewModel;
using Syncfusion.UI.Xaml.Scheduler;
using System.Windows;
using System.Windows.Controls;

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
                InitializeTimetable(timetableViewModel);
            }
        }

        private void InitializeTimetable(TimetableViewModel timetableViewModel)
        {
            Scheduler.DisplayDate = timetableViewModel.StartDateTime;

            DaysViewSettings daysViewSettings = Scheduler.DaysViewSettings;
            daysViewSettings.TimeInterval = timetableViewModel.TimeSpanPerCell;
        }
    }
}
