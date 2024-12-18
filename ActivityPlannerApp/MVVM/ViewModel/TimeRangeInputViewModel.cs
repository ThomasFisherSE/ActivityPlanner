using ActivityPlannerApp.MVVM.Model;

namespace ActivityPlannerApp.MVVM.ViewModel
{
    public class TimeRangeInputViewModel
    {
        public DateTime StartTimeAsDateTime { get; set; }

        public DateTime EndTimeAsDateTime { get; set; }

        public TimeOnly StartTime => TimeOnly.FromDateTime(StartTimeAsDateTime);

        public TimeOnly EndTime => TimeOnly.FromDateTime(EndTimeAsDateTime);

        public TimeRange TimeRange => new(StartTime, EndTime);

        public TimeRangeInputViewModel()
        {
            StartTimeAsDateTime = DateTime.Now;
            EndTimeAsDateTime = StartTimeAsDateTime.AddHours(1);
        }
    }
}
