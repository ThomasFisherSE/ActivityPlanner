using ActivityPlannerApp.Core;
using ActivityPlannerApp.MVVM.Model;
using System.Collections.ObjectModel;

namespace ActivityPlannerApp.MVVM.ViewModel
{
    class TimetableViewModel : ObservableObject
    {
        public ObservableCollection<TimetableEntry> TimetableEntries { get; set; } = [];

        public TimeSpan TimeSpanPerCell { get; set; } = TimeSpan.FromHours(1);

        public DateTime StartDateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// Constructor
        /// </summary>
        public TimetableViewModel()
        {

        }

        public TimetableEntry CreateTimetableEntry(ActivityModel activity, TimeSlot timeSlot)
        {
            TimetableEntry timetableEntry = new(timeSlot, activity.ActivityName, activity.Color);
            TimetableEntries.Add(timetableEntry);
            return timetableEntry;
        }

        public void PopulateTimetable(IList<ActivityModel> activities, ActivityTimingsModel activityTimingsModel)
        {
            TimetableEntries.Clear();

            foreach(ActivityModel activity in activities)
            {
                foreach(TimeSlot timeSlot in activityTimingsModel.GetTimes(activity))
                {
                    CreateTimetableEntry(activity, timeSlot);
                }
            }
        }
    }
}
