using ActivityPlannerApp.MVVM.Model;

namespace ActivityPlannerApp.MVVM.ViewModel
{
    public class SpecifyActivityTimesDialogViewModel
    {
        private readonly TimeRangeInputViewModel _timeRangeInputViewModel;

        public DateTime DateAsDateTime { get; set; } = DateTime.Today;

        public TimeRange TimeRange => _timeRangeInputViewModel.TimeRange;

        public DateOnly Date => DateOnly.FromDateTime(DateAsDateTime);

        public TimeSlot TimeSlot => new(Date, TimeRange);

        public SpecifyActivityTimesDialogViewModel(TimeRangeInputViewModel timeRangeInputViewModel)
        {
            _timeRangeInputViewModel = timeRangeInputViewModel;
        }
    }
}
