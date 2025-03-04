namespace ActivityPlannerApp.MVVM.Model
{
    public struct TimeSlot(DateOnly date, TimeRange timeRange)
    {
        public DateOnly Date { get; set; } = date;

        public TimeRange TimeRange { get; set; } = timeRange;

        public DateTime Start => new(Date, TimeRange.Start);

        public DateTime End => new(Date, TimeRange.End);

        public bool OverlapsWith(TimeSlot other) => Date == other.Date && TimeRange.OverlapsWith(other.TimeRange);
    }
}
