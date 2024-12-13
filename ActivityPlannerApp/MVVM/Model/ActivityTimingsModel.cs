namespace ActivityPlannerApp.MVVM.Model
{
    internal class ActivityTimingsModel
    {
        public Dictionary<ActivityModel, List<TimeSlot>> ActivityTimeSlots { get; } = [];

        public void AddActivityToTimeSlot(ActivityModel activity, TimeSlot timeSlot)
        {
            if (ActivityTimeSlots.TryGetValue(activity, out List<TimeSlot>? activityTimeSlots)
                && activityTimeSlots != null)
            {
                activityTimeSlots.Add(timeSlot);
            }
            else
            {
                activityTimeSlots = [timeSlot];
            }

            ActivityTimeSlots[activity] = activityTimeSlots;
        }

        public List<TimeSlot> GetTimes(ActivityModel activity)
        {
            return ActivityTimeSlots[activity];
        }
    }
}
