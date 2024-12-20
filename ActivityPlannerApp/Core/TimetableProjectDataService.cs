using ActivityPlannerApp.MVVM.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;

namespace ActivityPlannerApp.Core
{
    public static class TimetableProjectDataService
    {
        private static readonly string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        private static readonly string rootDataDirectoryPath = Path.Combine(appDataPath, "ActivityPlannerApp");

        private static readonly string projectDataFilePath = Path.Combine(rootDataDirectoryPath, "projectData.json");

        private static readonly TimetableProjectData emptyProjectData = new();

        private static readonly JsonSerializerOptions serializerOptions = new() 
        { 
            WriteIndented = true,
            ReferenceHandler = ReferenceHandler.Preserve
        };

        public static void SaveProjectData(TimetableProjectData timetableProjectData)
        {
            try
            {
                if (!Directory.Exists(rootDataDirectoryPath))
                    Directory.CreateDirectory(rootDataDirectoryPath);

                string projectJsonData = JsonSerializer.Serialize(timetableProjectData, serializerOptions);
                File.WriteAllText(projectDataFilePath, projectJsonData);
            } 
            catch (Exception ex)
            {
                ShowError($"Error while trying to save project data file ({projectDataFilePath}):{Environment.NewLine}{ex.Message}");
            }
        }

        public static TimetableProjectData LoadProjectData()
        {
            if (!File.Exists(projectDataFilePath))
                return emptyProjectData;

            try
            {
                string jsonData = File.ReadAllText(projectDataFilePath);
                TimetableProjectData? deserializedActivities = JsonSerializer.Deserialize<TimetableProjectData>(jsonData, serializerOptions);
                return deserializedActivities ?? emptyProjectData;
            } 
            catch (Exception ex)
            {
                ShowError($"Error while trying to load project data file ({projectDataFilePath}):{Environment.NewLine}{ex.Message}");
                return emptyProjectData;
            }
        }

        private static void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
