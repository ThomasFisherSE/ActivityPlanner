namespace ActivityPlannerApp.MVVM.Model
{
    internal class TimetableEntry
    {
        public TimeSlot TimeSlot { get; set; }

        public string Content { get; set; }

        private const string DefaultContent = "";

        public TimetableEntry(TimeSlot timeSlot, string content = DefaultContent)
        {
            TimeSlot = timeSlot;
            Content = content;
        }
    }
}
