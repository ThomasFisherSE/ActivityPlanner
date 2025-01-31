namespace ActivityPlannerApp.MVVM.Model
{
    public struct TimeSlot
    {
        public DateOnly Date { get; set; }

        public TimeRange TimeRange { get; set; }

        public DateTime Start => new DateTime(Date, TimeRange.Start);

        public DateTime End => new DateTime(Date, TimeRange.End);

        public TimeSlot(DateOnly date, TimeRange timeRange)
        {
            Date = date;
            TimeRange = timeRange;
        }

        public bool OverlapsWith(TimeSlot other) => Date == other.Date && TimeRange.OverlapsWith(other.TimeRange);
    }
}
