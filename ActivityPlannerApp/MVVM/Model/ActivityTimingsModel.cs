namespace ActivityPlannerApp.MVVM.Model
{
    public class ActivityTimingsModel
    {
        public IDictionary<Guid, IList<TimeSlot>> ActivityIdToTimeSlots { get; set; } = new Dictionary<Guid, IList<TimeSlot>>();

        public void AddActivityToTimeSlot(ActivityModel activity, TimeSlot timeSlot)
        {
            if (ActivityIdToTimeSlots.TryGetValue(activity.Id, out var activityTimeSlots))
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
