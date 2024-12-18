using ActivityPlannerApp.Core;
using ActivityPlannerApp.MVVM.Model;
using ActivityPlannerApp.MVVM.View;
using System.Collections.ObjectModel;

namespace ActivityPlannerApp.MVVM.ViewModel
{
    internal class MainViewModel
    {
        public ObservableCollection<ActivityModel> Activities { get; set; } = [];
        public ObservableCollection<LocationModel> Locations { get; set; } = [];

        public ActivityTimingsModel ActivityTimings { get; set; } = new ActivityTimingsModel();

        public TimetableViewModel TimetableViewModel { get; } = new TimetableViewModel();

        private bool _shouldAddTestData = true;

        public MainViewModel()
        {
            if (_shouldAddTestData)
                AddTestData();

            TimetableViewModel.PopulateTimetable(ActivityTimings);
        }

        public ActivityModel AddActivity(string activityName, LocationModel? location, string iconPath)
        {
            ActivityModel activity = new()
            {
                ActivityName = activityName,
                ActivityLocation = location,
                IconImageSource = iconPath
            };
            Activities.Add(activity);
            return activity;
        }

        public LocationModel AddLocation(string locationName)
        {
            LocationModel location = new()
            {
                LocationName = locationName
            };
            Locations.Add(location);
            return location;
        }

        public void AddActivityTiming(ActivityModel activity, TimeSlot timeSlot)
        {
            ActivityTimings.AddActivityToTimeSlot(activity, timeSlot);

            // Re-populate timetable
            // TODO: Just update the cells that need updating instead of repopulating the entire timetable
            TimetableViewModel.PopulateTimetable(ActivityTimings);
        }

        private void AddTestData()
        {
            if (Locations == null || Activities == null)
                return;

            // Locations
            LocationModel diner = new()
            {
                LocationName = "The Diner",
            };
            Locations.Add(diner);

            LocationModel sandwichShop = new()
            {
                LocationName = "The Sandwich Shop",
            };
            Locations.Add(sandwichShop);

            LocationModel restaurant = new()
            {
                LocationName = "The Restaurant",
            };
            Locations.Add(restaurant);

            // Activities
            ActivityModel breakfast = new()
            {
                ActivityName = "Breakfast",
                IconImageSource = "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fcdn-icons-png.flaticon.com%2F512%2F1046%2F1046745.png&f=1&nofb=1&ipt=206859ec619f71a18f53819bd7684d829d585ec3e8a8610b3108f3431dd82299&ipo=images",
                ActivityLocation = diner
            };
            Activities.Add(breakfast);

            ActivityModel lunch = new()
            {
                ActivityName = "Lunch",
                IconImageSource = "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fcdn-icons-png.flaticon.com%2F512%2F195%2F195815.png&f=1&nofb=1&ipt=964b34d14c6fe1eacb2594c4201e44a9426908cef2fbab979c21deac9f2ac2f6&ipo=images",
                ActivityLocation = sandwichShop
            };
            Activities.Add(lunch);

            ActivityModel dinner = new()
            {
                ActivityName = "Dinner",
                IconImageSource = "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fcdn3.iconfinder.com%2Fdata%2Ficons%2Fparty-44%2F64%2Fdinner-table-birthday-party-512.png&f=1&nofb=1&ipt=814c378cb0c6944ed44b685a65fa84595c26b870c1c4337099027201ae18c209&ipo=images",
                ActivityLocation = restaurant
            };
            Activities.Add(dinner);

            ActivityModel work = new()
            {
                ActivityName = "Work"
            };
            Activities.Add(work);

            // Activity time slots
            TimeRange breakfastTimeRange = new(new TimeOnly(9, 00), new TimeOnly(10, 00));
            TimeRange lunchTimeRange = new(new TimeOnly(12, 00), new TimeOnly(13, 00));
            TimeRange dinnerTimeRange = new(new TimeOnly(18, 00), new TimeOnly(19, 00));
            for (int i = 0; i < 7; i++)
            {
                DateOnly date = DateTimeUtils.Today.AddDays(i);
                ActivityTimings.AddActivityToTimeSlot(breakfast, new TimeSlot(date, breakfastTimeRange));
                ActivityTimings.AddActivityToTimeSlot(lunch, new TimeSlot(date, lunchTimeRange));
                ActivityTimings.AddActivityToTimeSlot(dinner, new TimeSlot(date, dinnerTimeRange));
            }
        }
    }
}
