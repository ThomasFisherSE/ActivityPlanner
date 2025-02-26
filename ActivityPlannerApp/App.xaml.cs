using Syncfusion.Licensing;

namespace ActivityPlannerApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
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
            var syncfusionLicense = Environment.GetEnvironmentVariable(SyncfusionLicenseVar);
            if (!string.IsNullOrEmpty(syncfusionLicense))
                SyncfusionLicenseProvider.RegisterLicense(syncfusionLicense);
        }
    }

}
