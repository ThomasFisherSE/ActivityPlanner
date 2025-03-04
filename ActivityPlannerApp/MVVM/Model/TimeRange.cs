namespace ActivityPlannerApp.MVVM.Model
{
    public struct TimeRange(TimeOnly start, TimeOnly end)
    {
        public TimeOnly Start { get; set; } = start;

        public TimeOnly End { get; set; } = end;

        /// <summary>
        /// Check if the <see cref="TimeRange"/> overlaps with another <see cref="TimeRange"/>
        /// </summary>
        /// <param name="other">The <see cref="TimeRange"/> to check for an overlap with</param>
        /// <returns>
        /// <c>true</c> if the <see cref="TimeRange"/> overlaps with the <see cref="other"/> <see cref="TimeRange"/>,
        /// <c>false</c> otherwise
        /// </returns>
        public bool OverlapsWith(TimeRange other) => Start < other.End && End > other.Start;

        public override string ToString()
        {
            return $"{Start} - {End}";
        }
    }
}
