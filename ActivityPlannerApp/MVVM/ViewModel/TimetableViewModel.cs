using ActivityPlannerApp.Core;
using ActivityPlannerApp.MVVM.Model;
using System.Collections.ObjectModel;

namespace ActivityPlannerApp.MVVM.ViewModel
{
    class TimetableViewModel : ObservableObject
    {
        public ObservableCollection<TimetableEntry> TimetableEntries { get; set; } = [];

        public TimeSpan TimeSpanPerCell { get; set; } = TimeSpan.FromHours(1);

        public DateTime StartDateTime { get; set; } = DateTime.Now;

        public int NumberOfDayColumns { get; set; } = 7;

        public IList<DateOnly> ColumnDays { get; }

        public IList<TimeRange> RowTimeRanges { get; }

        public TimetableViewModel()
        {
            ColumnDays = GenerateColumnDays(DateOnly.FromDateTime(StartDateTime), NumberOfDayColumns);
            RowTimeRanges = GenerateRowTimeRanges(TimeSpanPerCell);
        }

        public TimetableEntry CreateTimetableEntry(ActivityModel activity, TimeSlot timeSlot)
        {
            TimetableEntry timetableEntry = new(timeSlot, activity.ActivityName, activity.Color);
            TimetableEntries.Add(timetableEntry);
            return timetableEntry;
        }

        public void PopulateTimetable(IList<ActivityModel> activities, ActivityTimingsModel activityTimingsModel)
        {
            TimetableEntries.Clear();

            foreach(ActivityModel activity in activities)
            {
                foreach(TimeSlot timeSlot in activityTimingsModel.GetTimes(activity))
                {
                    CreateTimetableEntry(activity, timeSlot);
                }
            }
        }

        public IList<TimeRange> GenerateRowTimeRanges(TimeSpan timeSpan)
        {
            IList<TimeRange> timeRanges = [];

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

        public IList<DateOnly> GenerateColumnDays(DateOnly startDate, int numberOfDays)
        {
            IList<DateOnly> days = [];
            for (int i = 0; i < numberOfDays; i++)
            {
                days.Add(startDate.AddDays(i));
            }
            return days;
        }
    }
}
