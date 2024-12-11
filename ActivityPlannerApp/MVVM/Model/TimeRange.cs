namespace ActivityPlannerApp.MVVM.Model
{
    struct TimeRange
    {
        public TimeOnly Start { get; }

        public TimeOnly End { get; }

        public TimeRange(TimeOnly start, TimeOnly end)
        {
            Start = start; 
            End = end;
        }

        public override string ToString()
        {
            return $"{Start} - {End}";
        }
    }
}
