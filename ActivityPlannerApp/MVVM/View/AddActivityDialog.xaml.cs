using ActivityPlannerApp.MVVM.Model;
using ActivityPlannerApp.MVVM.ViewModel;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Windows;

namespace ActivityPlannerApp.MVVM.View
{
    /// <summary>
    /// Interaction logic for AddActivityDialog.xaml
    /// </summary>
    public partial class AddActivityDialog : Window
    {
        public AddActivityDialogViewModel ViewModel { get; private set; }

        public AddActivityDialog(ObservableCollection<LocationModel> locationOptions)
        {
            InitializeComponent();

            DataContext = ViewModel = new AddActivityDialogViewModel(locationOptions);
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

        private void IconButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "Image files (*.png;*.jpg)|*.png;*.jpg|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true && !string.IsNullOrWhiteSpace(openFileDialog.FileName))
            {
                ViewModel.IconPath = openFileDialog.FileName;
            }
        }
    }
}
