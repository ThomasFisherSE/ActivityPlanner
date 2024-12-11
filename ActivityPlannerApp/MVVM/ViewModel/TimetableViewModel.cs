using ActivityPlannerApp.Core;
using ActivityPlannerApp.MVVM.Model;
using System.Collections.ObjectModel;

namespace ActivityPlannerApp.MVVM.ViewModel
{
    class TimetableViewModel : ObservableObject
    {
        public ObservableCollection<ActivitySlot> ActivitySlots { get; set; }

        public TimeSpan TimeSpanPerCell { get; set; } = TimeSpan.FromHours(1);

        public DateTime StartDateTime { get; set; } = DateTime.Now;

        public int NumberOfDayColumns { get; set; } = 7;

        public List<DateOnly> Days { get; }

        public List<TimeRange> TimeRanges { get; }

        public TimetableViewModel()
        {
            Days = GenerateDays(DateOnly.FromDateTime(StartDateTime), NumberOfDayColumns);
            TimeRanges = GenerateTimeRanges(TimeSpanPerCell);
            ActivitySlots = GenerateActivitySlots();
        }

        public List<TimeRange> GenerateTimeRanges(TimeSpan timeSpan)
        {
            List<TimeRange> timeRanges = new List<TimeRange>();

            TimeOnly startTime = TimeOnly.MinValue;
            bool isEndOfDay = false;
            do
            {
                TimeOnly endTime = startTime.Add(timeSpan, out int wrappedDays);

                // Check if the end of the day was reached, in which case, cut the time range short
                if (wrappedDays > 0)
                {
                    endTime = TimeOnly.MaxValue;
                    isEndOfDay = true;
                }

                timeRanges.Add(new TimeRange(startTime, endTime));

                // Start the next range at the end of the previous range
                startTime = endTime;
            } while (!isEndOfDay);

            return timeRanges;
        }

        public List<DateOnly> GenerateDays(DateOnly startDate, int numberOfDays)
        {
            List<DateOnly> days = new List<DateOnly>();
            for (int i = 0; i < numberOfDays; i++)
            {
                days.Add(startDate.AddDays(i));
            }
            return days;
        }

        public ObservableCollection<ActivitySlot> GenerateActivitySlots()
        {
            ObservableCollection<ActivitySlot> activitySlots = new ObservableCollection<ActivitySlot>();
            foreach(DateOnly day in Days)
            {
                foreach(TimeRange timeRange in TimeRanges)
                {
                    ActivitySlot activitySlot = new ActivitySlot(day, timeRange);
                    activitySlot.Activity = ActivityModel.DefaultActivity;
                    activitySlots.Add(activitySlot);
                }
            }
            return activitySlots;
        }
    }
}
