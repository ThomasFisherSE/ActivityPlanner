using ActivityPlannerApp.Core;
using ActivityPlannerApp.MVVM.Model;
using System.Collections.ObjectModel;

namespace ActivityPlannerApp.MVVM.ViewModel
{
    class TimetableViewModel : ObservableObject
    {
        public ObservableCollection<TimetableCell> TimetableCells { get; set; }

        public TimeSpan TimeSpanPerCell { get; set; } = TimeSpan.FromHours(1);

        public DateTime StartDateTime { get; set; } = DateTime.Now;

        public int NumberOfDayColumns { get; set; } = 7;

        public IList<DateOnly> Days { get; }

        public IList<TimeRange> TimeRanges { get; }

        public TimetableViewModel()
        {
            Days = GenerateDays(DateOnly.FromDateTime(StartDateTime), NumberOfDayColumns);
            TimeRanges = GenerateTimeRanges(TimeSpanPerCell);
            TimetableCells = GenerateTimetableCells();
        }

        public IList<TimeRange> GenerateTimeRanges(TimeSpan timeSpan)
        {
            IList<TimeRange> timeRanges = new List<TimeRange>();

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

        public IList<DateOnly> GenerateDays(DateOnly startDate, int numberOfDays)
        {
            IList<DateOnly> days = new List<DateOnly>();
            for (int i = 0; i < numberOfDays; i++)
            {
                days.Add(startDate.AddDays(i));
            }
            return days;
        }

        public ObservableCollection<TimetableCell> GenerateTimetableCells()
        {
            ObservableCollection<TimetableCell> timetableCells = new ObservableCollection<TimetableCell>();
            foreach(DateOnly day in Days)
            {
                foreach(TimeRange timeRange in TimeRanges)
                {
                    TimeSlot timeSlot = new TimeSlot(day, timeRange);
                    timetableCells.Add(new TimetableCell(timeSlot));
                }
            }
            return timetableCells;
        }
    }
}
