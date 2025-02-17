using ActivityPlannerApp.Core;
using ActivityPlannerApp.MVVM.Model;
using ActivityPlannerApp.MVVM.View;
using ActivityPlannerApp.MVVM.ViewModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ActivityPlannerApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            DataContextChanged += MainWindow_DataContextChanged;
            CacheMainViewModel();

            if (_viewModel == null)
                throw new NullReferenceException($"{nameof(MainWindow)} view model is null");
        }

        private void CacheMainViewModel()
        {
            if (DataContext is MainViewModel viewModel)
            {
                _viewModel = viewModel;
            }
            else
            {
                DataContext = _viewModel = new MainViewModel();
            }
        }

        private void MainWindow_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            CacheMainViewModel();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            _viewModel.SaveProjectData();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void ButtonMaximize_Click(object sender, RoutedEventArgs e)
        {
            Window mainWindow = Application.Current.MainWindow;
            if (mainWindow.WindowState != WindowState.Maximized)
                mainWindow.WindowState = WindowState.Maximized;
            else
                mainWindow.WindowState = WindowState.Normal;
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ButtonAddActivity_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainViewModel mainViewModel)
            {
                AddActivityDialog addActivityDialog = new(mainViewModel.Locations);
                if (addActivityDialog.ShowDialog() == true)
                {
                    AddActivityDialogViewModel addActivityDialogViewModel = addActivityDialog.ViewModel;
                    mainViewModel.AddActivity(
                        addActivityDialogViewModel.ActivityName,
                        addActivityDialogViewModel.SelectedLocation,
                        addActivityDialogViewModel.IconPath);
                }
            }
        }

        private void ButtonAddLocation_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainViewModel mainViewModel)
            {
                AddLocationDialog addLocationDialog = new();
                if (addLocationDialog.ShowDialog() == true)
                {
                    AddLocationDialogViewModel addLocationDialogViewModel = addLocationDialog.ViewModel;
                    mainViewModel.AddLocation(addLocationDialogViewModel.LocationName, addLocationDialogViewModel.LocationColor);
                }
            }
        }

        private void ActivitiesList_ActivityEditRequested(object sender, ActivitySelectionEventArgs eventArgs)
        {
            if (DataContext is MainViewModel mainViewModel)
            {
                SpecifyActivityTimesDialog specifyActivityTimesDialog = new();
                if (specifyActivityTimesDialog.ShowDialog() == true)
                {
                    SpecifyActivityTimesDialogViewModel specifyActivityTimesDialogViewModel = specifyActivityTimesDialog.ViewModel;
                    mainViewModel.AddActivityTiming(eventArgs.Activity, specifyActivityTimesDialogViewModel.TimeSlot);
                }
            }
        }
    }
}