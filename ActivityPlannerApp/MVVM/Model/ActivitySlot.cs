namespace ActivityPlannerApp.MVVM.Model
{
    internal class ActivitySlot
    {
        public DateOnly Date { get; set; }

        public TimeRange TimeRange { get; set; }

        public ActivityModel? Activity { get; set; }

        public string ActivityString => Activity != null ? Activity.ActivityName : string.Empty;

        public ActivitySlot(DateOnly date, TimeRange timeRange)
        {
            Date = date;
            TimeRange = timeRange;
        }
    }
}
