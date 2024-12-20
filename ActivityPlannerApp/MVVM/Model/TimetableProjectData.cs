namespace ActivityPlannerApp.MVVM.Model
{
    public struct TimetableProjectData
    {
        public IList<ActivityModel> Activities { get; set; } = Array.Empty<ActivityModel>();

        public IList<LocationModel> Locations { get; set; } = Array.Empty<LocationModel>();

        public ActivityTimingsModel ActivityTimings { get; set; } = new ActivityTimingsModel();

        public TimetableProjectData()
        {

        }
    }
}
