using ActivityPlannerApp.Core;
using ActivityPlannerApp.MVVM.Model;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace ActivityPlannerApp.MVVM.ViewModel
{
    internal class MainViewModel
    {
        public ObservableCollection<ActivityModel> Activities { get; set; } = [];
        public ObservableCollection<LocationModel> Locations { get; set; } = [];

        public ActivityTimingsModel ActivityTimings { get; set; } = new ActivityTimingsModel();

        public TimetableViewModel TimetableViewModel { get; } = new TimetableViewModel();

        public MainViewModel()
        {
            LoadData();

            TimetableViewModel.PopulateTimetable(Activities, ActivityTimings);
        }

        public ActivityModel AddActivity(string activityName, LocationModel? location, string iconPath)
        {
            ActivityModel activity = new()
            {
                ActivityName = activityName,
                ActivityLocation = location
            };

            if (!string.IsNullOrEmpty(iconPath))
                activity.IconImageSource = iconPath;

            Activities.Add(activity);
            return activity;
        }

        public LocationModel AddLocation(string locationName, Color color)
        {
            LocationModel location = new()
            {
                LocationName = locationName,
                Color = color
            };
            Locations.Add(location);
            return location;
        }

        public void AddActivityTiming(ActivityModel activity, TimeSlot timeSlot)
        {
            ActivityTimings.AddActivityToTimeSlot(activity, timeSlot);
            TimetableViewModel.CreateTimetableEntry(activity, timeSlot);
        }

        public void SaveProjectData()
        {
            TimetableProjectData timetableProjectData = new()
            {
                Activities = Activities,
                Locations = Locations,
                ActivityTimings = ActivityTimings
            };

            TimetableProjectDataService.SaveProjectData(timetableProjectData);
        }

        public void LoadData()
        {
            TimetableProjectData timetableProjectData = TimetableProjectDataService.LoadProjectData();
            Activities = new ObservableCollection<ActivityModel>(timetableProjectData.Activities);
            Locations = new ObservableCollection<LocationModel>(timetableProjectData.Locations);
            ActivityTimings = timetableProjectData.ActivityTimings;
        }
    }
}
