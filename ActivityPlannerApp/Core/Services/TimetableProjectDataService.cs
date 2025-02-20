using ActivityPlannerApp.MVVM.Model;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;

namespace ActivityPlannerApp.Core.Services
{
    /// <summary>
    /// Service for saving and loading timetable project data
    /// </summary>
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

        /// <summary>
        /// Saves project data to file
        /// </summary>
        /// <param name="timetableProjectData">The <see cref="TimetableProjectData"/> to save</param>
        public static void SaveProjectData(TimetableProjectData timetableProjectData)
        {
            try
            {
                if (!Directory.Exists(rootDataDirectoryPath))
                    Directory.CreateDirectory(rootDataDirectoryPath);

                string projectJsonData = SerializeProjectDataToJson(timetableProjectData);
                File.WriteAllText(projectDataFilePath, projectJsonData);
            }
            catch (Exception ex)
            {
                ShowError($"Error while trying to save project data file ({projectDataFilePath}):{Environment.NewLine}{ex.Message}");
            }
        }

        /// <summary>
        /// Loads project data from file
        /// </summary>
        /// <returns>The loaded <see cref="TimetableProjectData"/></returns>
        public static TimetableProjectData LoadProjectData()
        {
            if (!File.Exists(projectDataFilePath))
                return emptyProjectData;

            try
            {
                string jsonData = File.ReadAllText(projectDataFilePath);
                return DeserializeProjectDataFromJson(jsonData);
            }
            catch (Exception ex)
            {
                ShowError($"Error while trying to load project data file ({projectDataFilePath}):{Environment.NewLine}{ex.Message}");
                return emptyProjectData;
            }
        }

        /// <summary>
        /// Serializes the given <see cref="TimetableProjectData"/> into JSON
        /// </summary>
        /// <param name="timetableProjectData">The <see cref="TimetableProjectData"/> to serialize</param>
        /// <returns>The serialized JSON data</returns>
        public static string SerializeProjectDataToJson(TimetableProjectData timetableProjectData)
        {
            return JsonSerializer.Serialize(timetableProjectData, serializerOptions);
        }

        /// <summary>
        /// Deserializes the given JSON <see cref="string"/> into a <see cref="TimetableProjectData"/> object
        /// </summary>
        /// <param name="jsonData">The JSON <see cref="string"/> to deserialize</param>
        /// <returns>The deserialized <see cref="TimetableProjectData"/></returns>
        public static TimetableProjectData DeserializeProjectDataFromJson(string jsonData)
        {
            TimetableProjectData? deserializedActivities = JsonSerializer.Deserialize<TimetableProjectData>(jsonData, serializerOptions);
            return deserializedActivities ?? emptyProjectData;
        }

        private static void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
