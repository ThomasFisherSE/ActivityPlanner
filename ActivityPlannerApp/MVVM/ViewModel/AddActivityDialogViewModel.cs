using ActivityPlannerApp.MVVM.Model;
using System.Collections.ObjectModel;

namespace ActivityPlannerApp.MVVM.ViewModel
{
    public class AddActivityDialogViewModel(ObservableCollection<LocationModel> locationOptions)
    {
        public string ActivityName { get; set; } = string.Empty;

        public string IconPath { get; set; } = string.Empty;

        public LocationModel? SelectedLocation { get; set; } = null;

        public ObservableCollection<LocationModel> LocationOptions { get; set; } = locationOptions;
    }
}
