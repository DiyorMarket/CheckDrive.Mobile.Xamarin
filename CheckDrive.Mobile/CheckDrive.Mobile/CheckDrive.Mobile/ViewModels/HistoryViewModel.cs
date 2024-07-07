using CheckDrive.ApiContracts.Driver;
using CheckDrive.Mobile.Helpers;
using CheckDrive.Mobile.Services;
using CheckDrive.Web.Stores.Drivers;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CheckDrive.Mobile.ViewModels
{
    public class HistoryViewModel : BaseViewModel
    {
        private readonly IDriverDataStore _driverDataStore;
        private readonly DriverDto _driver = DataService.GetAccount();

        public ObservableCollection<History> Reviews { get; private set; }

        public HistoryViewModel(IDriverDataStore driverDataStore)
        {
            _driverDataStore = driverDataStore;

            Reviews = new ObservableCollection<History>();

            LoadViewPage();
        }

        public async void LoadViewPage()
        {

            IsBusy = true;

            await ShowHistory();

            IsBusy = false;
        }

        private async Task ShowHistory()
        {
            var driverHistories = await _driverDataStore.GetDriverHistoryDtosAsync(_driver.Id);

            if(driverHistories != null)
            {
                foreach (var driverHistory in driverHistories)
                {
                    Reviews.Add(new History()
                    {
                        Date = driverHistory.Date,
                        IsHealthy = driverHistory.IsHealthy,
                        IsHanded = driverHistory.IsHanded,
                        IsGiven = driverHistory.IsGiven,
                        IsAccepted = driverHistory.IsAccepted,
                    });
                }
            }
        }
    }
}
