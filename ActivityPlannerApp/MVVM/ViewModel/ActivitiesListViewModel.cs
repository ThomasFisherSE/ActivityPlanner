using ActivityPlannerApp.MVVM.Model;
using System.Collections.ObjectModel;

namespace ActivityPlannerApp.MVVM.ViewModel
{
    public class ActivitiesListViewModel
    {
        public ObservableCollection<ActivityModel> Activities { get; set; } = [];
    }
}
