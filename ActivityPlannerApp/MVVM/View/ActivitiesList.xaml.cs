using ActivityPlannerApp.Core;
using ActivityPlannerApp.MVVM.Model;
using ActivityPlannerApp.MVVM.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ActivityPlannerApp.MVVM.View
{
    /// <summary>
    /// Interaction logic for ActivitiesList.xaml
    /// </summary>
    public partial class ActivitiesList : UserControl
    {
        public event EventHandler<ActivitySelectionEventArgs>? ActivityEditRequested;

        public ActivitiesList()
        {
            InitializeComponent();
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ListView.SelectedItem is ActivityModel selectedActivity)
            {
                ActivityEditRequested?.Invoke(this, new ActivitySelectionEventArgs(selectedActivity));
            }
        }
    }
}
