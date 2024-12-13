namespace ActivityPlannerApp.MVVM.Model
{
    internal class TimetableCell
    {
        public TimeSlot TimeSlot { get; set; }

        public string Content { get; set; } = string.Empty;

        private const string DefaultContent = "";

        public TimetableCell(TimeSlot timeSlot, string content = DefaultContent)
        {
            TimeSlot = timeSlot;
            Content = content;
        }
    }
}
