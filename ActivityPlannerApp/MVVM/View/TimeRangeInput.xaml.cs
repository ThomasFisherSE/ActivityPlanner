using ActivityPlannerApp.MVVM.ViewModel;
using System.Windows.Controls;

namespace ActivityPlannerApp.MVVM.View
{
    /// <summary>
    /// Interaction logic for TimeRangeInput.xaml
    /// </summary>
    public partial class TimeRangeInput : UserControl
    {
        public TimeRangeInputViewModel ViewModel { get; set; }

        public TimeRangeInput()
        {
            InitializeComponent();

            DataContext = ViewModel = new TimeRangeInputViewModel();
        }
    }
}
