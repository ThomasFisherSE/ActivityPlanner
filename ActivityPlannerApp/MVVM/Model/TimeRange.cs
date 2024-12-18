namespace ActivityPlannerApp.MVVM.Model
{
    public struct TimeRange
    {
        public TimeOnly Start { get; set; }

        public TimeOnly End { get; set; }

        public TimeRange(TimeOnly start, TimeOnly end)
        {
            Start = start; 
            End = end;
        }

        /// <summary>
        /// Check if the <see cref="TimeRange"/> overlaps with another <see cref="TimeRange"/>
        /// </summary>
        /// <param name="other">The <see cref="TimeRange"/> to check for an overlap with</param>
        /// <returns>
        /// <c>true</c> if the <see cref="TimeRange"/> overlaps with the <see cref="other"/> <see cref="TimeRange"/></c>, <c>false</c> otherwise
        /// </returns>
        public bool OverlapsWith(TimeRange other) => Start < other.End && End > other.Start;

        public override string ToString()
        {
            return $"{Start} - {End}";
        }
    }
}
