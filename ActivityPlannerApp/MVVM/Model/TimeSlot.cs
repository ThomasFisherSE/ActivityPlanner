namespace ActivityPlannerApp.MVVM.Model
{
    internal struct TimeSlot
    {
        public DateOnly Date { get; set; }

        public TimeRange TimeRange { get; set; }

        public TimeSlot(DateOnly date, TimeRange timeRange)
        {
            Date = date;
            TimeRange = timeRange;
        }
    }
}
