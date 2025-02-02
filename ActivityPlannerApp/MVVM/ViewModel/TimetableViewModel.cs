﻿using ActivityPlannerApp.Core;
using ActivityPlannerApp.MVVM.Model;
using System.Collections.ObjectModel;

namespace ActivityPlannerApp.MVVM.ViewModel
{
    class TimetableViewModel : ObservableObject
    {
        public ObservableCollection<TimetableEntryGridInfo> TimetableEntryGridInfoCollection { get; set; } = [];

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

        public void PopulateTimetable(IList<ActivityModel> activities, ActivityTimingsModel activityTimingsModel)
        {
            foreach(ActivityModel activity in activities)
            {
                foreach(TimeSlot timeSlot in activityTimingsModel.GetTimes(activity))
                {
                    TimetableEntry timetableEntry = new(timeSlot, activity.ActivityName);

                    if (TryDetermineGridInfo(timetableEntry, out int row, out int column, out int rowSpan))
                    {
                        TimetableEntryGridInfo timetableEntryGridInfo = new(timetableEntry, row, column, rowSpan);
                        TimetableEntryGridInfoCollection.Add(timetableEntryGridInfo);
                    }
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

        private bool TryDetermineGridInfo(TimetableEntry timetableEntry, out int row, out int column, out int rowSpan)
        {
            TimeSlot timeSlot = timetableEntry.TimeSlot;
            if (!TryDetermineGridColumn(timeSlot.Date, out column) || 
                !TryDetermineGridRowInfo(timeSlot.TimeRange, out row, out rowSpan))
            {
                column = -1;
                row = -1;
                rowSpan = -1;
                return false;
            }

            return true;
        }

        private bool TryDetermineGridRowInfo(TimeRange timeRange, out int row, out int rowSpan)
        {
            for (int i = 0; i < RowTimeRanges.Count; i++)
            {
                if (DoesTimeRowOverlapWithTimeRange(i, timeRange))
                {
                    row = i + 1; // +1 because of header row

                    // Found the first row, now check how many rows it overlaps
                    rowSpan = 1;
                    while (DoesTimeRowOverlapWithTimeRange(i+1, timeRange))
                    {
                        i++;
                        rowSpan++;
                    }
                    return true;
                }
            }

            row = -1;
            rowSpan = -1;
            return false;
        }

        private bool TryDetermineGridColumn(DateOnly day, out int col)
        {
            col = ColumnDays.IndexOf(day) + 1; // +1 because of header column
            return col > 0;
        }

        private bool DoesTimeRowOverlapWithTimeRange(int rowIndex, TimeRange timeRange)
        {
            if (rowIndex >= RowTimeRanges.Count)
                return false;

            return timeRange.OverlapsWith(RowTimeRanges[rowIndex]);
        }
    }
}
