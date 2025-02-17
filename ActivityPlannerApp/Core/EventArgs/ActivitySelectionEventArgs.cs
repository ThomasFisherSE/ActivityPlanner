using ActivityPlannerApp.MVVM.Model;

namespace ActivityPlannerApp.Core
{
    public class ActivitySelectionEventArgs(ActivityModel activity) : EventArgs
    {
        public ActivityModel Activity { get; set; } = activity;
    }
}
