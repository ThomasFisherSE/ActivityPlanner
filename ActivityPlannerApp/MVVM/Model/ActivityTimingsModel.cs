namespace ActivityPlannerApp.MVVM.Model
{
    public class ActivityTimingsModel
    {
        public Dictionary<Guid, IList<TimeSlot>> ActivityIdToTimeSlots { get; } = [];

        public void AddActivityToTimeSlot(ActivityModel activity, TimeSlot timeSlot)
        {
            if (ActivityIdToTimeSlots.TryGetValue(activity.Id, out IList<TimeSlot>? activityTimeSlots)
                && activityTimeSlots != null)
            {
                activityTimeSlots.Add(timeSlot);
            }
            else
            {
                activityTimeSlots = [timeSlot];
            }

            ActivityIdToTimeSlots[activity.Id] = activityTimeSlots;
        }

        public IList<TimeSlot> GetTimes(ActivityModel activity)
        {
            return ActivityIdToTimeSlots.TryGetValue(activity.Id, out IList<TimeSlot>? value) ? value : [];
        }
    }
}
