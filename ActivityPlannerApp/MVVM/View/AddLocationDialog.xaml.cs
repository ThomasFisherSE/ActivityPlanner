using ActivityPlannerApp.MVVM.ViewModel;
using System.Windows;

namespace ActivityPlannerApp.MVVM.View
{
    /// <summary>
    /// Interaction logic for AddLocationDialog.xaml
    /// </summary>
    public partial class AddLocationDialog : Window
    {
        public AddLocationDialogViewModel ViewModel { get; set; }

        public AddLocationDialog()
        {
            InitializeComponent();

            DataContext = ViewModel = new AddLocationDialogViewModel();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
