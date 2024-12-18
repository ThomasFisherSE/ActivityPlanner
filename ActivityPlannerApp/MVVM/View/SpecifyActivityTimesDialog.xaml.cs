using ActivityPlannerApp.MVVM.ViewModel;
using System.Windows;

namespace ActivityPlannerApp.MVVM.View
{
    /// <summary>
    /// Interaction logic for SpecifyActivityTimesDialog.xaml
    /// </summary>
    public partial class SpecifyActivityTimesDialog : Window
    {
        public SpecifyActivityTimesDialogViewModel ViewModel { get; set; }

        public SpecifyActivityTimesDialog()
        {
            InitializeComponent();

            DataContext = ViewModel = new SpecifyActivityTimesDialogViewModel(TimeRangeInput.ViewModel);
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
