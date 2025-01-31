using Syncfusion.Licensing;
using System.Windows;

namespace ActivityPlannerApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string SyncfusionLicenseVar = "SYNCFUSION_LICENSE";

        public App()
        {
            RegisterSyncfusionLicense();
        }

        private static void RegisterSyncfusionLicense()
        {
            // Use CI to provide license key in published builds, but for developer environment,
            // use environment variable to determine Syncfusion license key
            string? syncfusionLicense = Environment.GetEnvironmentVariable(SyncfusionLicenseVar);
            if (!string.IsNullOrEmpty(syncfusionLicense))
                SyncfusionLicenseProvider.RegisterLicense(syncfusionLicense);
        }
    }

}
