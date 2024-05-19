using CheckDrive.Mobile.Services;
using CheckDrive.Mobile.Views;
using Xamarin.Forms;

namespace CheckDrive.Mobile
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzI3MDgyNEAzMjM1MmUzMDJlMzBIU2RvbkFWNUp2R0FwNDBnYi9yUFFROExGcGVmc0c3NU56bDBhaU85SGZnPQ==");

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
