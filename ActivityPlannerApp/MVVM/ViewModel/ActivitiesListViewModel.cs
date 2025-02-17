using ActivityPlannerApp.MVVM.Model;
using ActivityPlannerApp.MVVM.View;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ActivityPlannerApp.MVVM.ViewModel
{
    public class ActivitiesListViewModel
    {
        public ObservableCollection<ActivityModel> Activities { get; set; } = [];
    }
}
