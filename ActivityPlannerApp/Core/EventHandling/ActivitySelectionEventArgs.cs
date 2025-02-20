using ActivityPlannerApp.MVVM.Model;

namespace ActivityPlannerApp.Core.EventHandling
{
    public class ActivitySelectionEventArgs(ActivityModel activity) : EventArgs
    {
        public ActivityModel Activity { get; set; } = activity;
    }
}
