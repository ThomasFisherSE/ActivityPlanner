using ActivityPlannerApp.MVVM.Model;
using System.Windows.Media;

namespace ActivityPlannerApp.MVVM.ViewModel
{
    public class AddLocationDialogViewModel
    {
        public string LocationName { get; set; } = string.Empty;

        public Color LocationColor { get; set; } = LocationModel.DefaultLocationColor;
    }
}
