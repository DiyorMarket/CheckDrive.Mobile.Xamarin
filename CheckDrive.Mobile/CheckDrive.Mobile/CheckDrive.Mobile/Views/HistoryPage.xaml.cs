using CheckDrive.Mobile.Services;
using CheckDrive.Mobile.Stores.DoctorReviews;
using CheckDrive.Mobile.Stores.Drivers;
using CheckDrive.Mobile.Stores.MechanicAcceptances;
using CheckDrive.Mobile.Stores.MechanicHandovers;
using CheckDrive.Mobile.Stores.OperatorReviews;
using CheckDrive.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CheckDrive.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryPage : ContentPage
    {
        public HistoryPage()
        {
            InitializeComponent();

            var client = new ApiClient();
            var driverDataStore = new DriverDataStore(client);

            BindingContext = new HistoryViewModel(driverDataStore);
        }

        private void HistoryRefresh_Refreshing(object sender, System.EventArgs e)
        {
            var client = new ApiClient();
            var driverDataStore = new DriverDataStore(client);

            BindingContext = new HistoryViewModel(driverDataStore);

            HistoryPageRefresh.IsRefreshing = false;
        }
    }
}
