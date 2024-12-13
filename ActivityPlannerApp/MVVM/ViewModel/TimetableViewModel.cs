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

        public IList<DateOnly> DayColumns { get; }

        public IList<TimeRange> TimeRangeRows { get; }

        public TimetableViewModel()
        {
            DayColumns = GenerateDayColumnDates(DateOnly.FromDateTime(StartDateTime), NumberOfDayColumns);
            TimeRangeRows = GenerateTimeRanges(TimeSpanPerCell);
            TimetableCells = GenerateTimetableCells();
        }

        public void PopulateTimetable(ActivityTimingsModel activityTimingsModel)
        {
            foreach(ActivityModel activity in activityTimingsModel.ActivityTimeSlots.Keys)
            {
                // Determine the cells that should contain this activity
                List<TimeSlot> timeSlots = activityTimingsModel.GetTimes(activity);
                IList<TimetableCell> timetableCells = GetTimetableCells(timeSlots);
                foreach(TimetableCell timetableCell in timetableCells)
                {
                    timetableCell.Content = activity.ActivityName;
                }
            }
        }

        public IList<TimeRange> GenerateTimeRanges(TimeSpan timeSpan)
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

        public IList<DateOnly> GenerateDayColumnDates(DateOnly startDate, int numberOfDays)
        {
            IList<DateOnly> days = [];
            for (int i = 0; i < numberOfDays; i++)
            {
                days.Add(startDate.AddDays(i));
            }
            return days;
        }

        public ObservableCollection<TimetableCell> GenerateTimetableCells()
        {
            ObservableCollection<TimetableCell> timetableCells = [];
            foreach(DateOnly day in DayColumns)
            {
                foreach(TimeRange timeRange in TimeRangeRows)
                {
                    TimeSlot timeSlot = new(day, timeRange);
                    timetableCells.Add(new TimetableCell(timeSlot));
                }
            }
            return timetableCells;
        }

        private IList<TimetableCell> GetTimetableCells(IList<TimeSlot> timeSlots)
        {
            IList<TimetableCell> overlappingCells = [];
            foreach(TimetableCell timetableCell in TimetableCells)
            {
                foreach(TimeSlot timeSlot in timeSlots)
                {
                    if (timetableCell.TimeSlot.OverlapsWith(timeSlot))
                        overlappingCells.Add(timetableCell);
                }
            }
            return overlappingCells;
        }
    }
}
