namespace ActivityPlannerApp.Core
{
    internal static class DateTimeUtils
    {
        public static DateOnly Today => DateOnly.FromDateTime(DateTime.Now);

        public static TimeOnly CurrentTime => TimeOnly.FromDateTime(DateTime.Now);
    }
}
