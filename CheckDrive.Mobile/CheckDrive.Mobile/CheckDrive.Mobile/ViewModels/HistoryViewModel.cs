using CheckDrive.ApiContracts;
using CheckDrive.ApiContracts.Driver;
using CheckDrive.Mobile.Helpers;
using CheckDrive.Mobile.Services;
using CheckDrive.Web.Stores.DoctorReviews;
using CheckDrive.Web.Stores.MechanicAcceptances;
using CheckDrive.Web.Stores.MechanicHandovers;
using CheckDrive.Web.Stores.OperatorReviews;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace CheckDrive.Mobile.ViewModels
{
    public class HistoryViewModel : BaseViewModel
    {
        private readonly IDoctorReviewDataStore _doctorReviewDataStore;
        private readonly IMechanicHandoverDataStore _mechanicHandoverDataStore;
        private readonly IOperatorReviewDataStore _operatorReviewDataStore;
        private readonly IMechanicAcceptanceDataStore _mechanicAcceptanceDataStore;
        private readonly DriverDto _driver = DataService.GetAccount();

        public ObservableCollection<History> Reviews { get; private set; }

        public HistoryViewModel(IDoctorReviewDataStore doctorReviewDataStore, IMechanicHandoverDataStore mechanicHandoverDataStore, IOperatorReviewDataStore operatorReviewDataStore, IMechanicAcceptanceDataStore mechanicAcceptanceDataStore)
        {
            _doctorReviewDataStore = doctorReviewDataStore;
            _mechanicHandoverDataStore = mechanicHandoverDataStore;
            _operatorReviewDataStore = operatorReviewDataStore;
            _mechanicAcceptanceDataStore = mechanicAcceptanceDataStore;

            Reviews = new ObservableCollection<History>();

            LoadViewPage();
        }

        public async void LoadViewPage()
        {

            IsBusy = true;

            await ShowHistoryOperations();

            IsBusy = false;
        }

        public async Task ShowHistoryOperations()
        {
            var doctorItemsResponse = await _doctorReviewDataStore.GetDoctorReviewsByDriverIdAsync(_driver.Id);
            var doctorItems = doctorItemsResponse.Data.ToList();
            var mechanicHandoverItemsResponse = await _mechanicHandoverDataStore.GetMechanicHandoversByDriverIdAsync(_driver.Id);
            var mechanicHandoverItems = mechanicHandoverItemsResponse.Data.ToList();
            var operatorItemsResponse = await _operatorReviewDataStore.GetOperatorReviewsByDriverIdAsync(_driver.Id);
            var operatorItems = operatorItemsResponse.Data.ToList();
            var mechanicAcceptenceResponse = await _mechanicAcceptanceDataStore.GetMechanicAcceptancesByDriverIdAsync(_driver.Id);
            var mechanicAcceptence = mechanicAcceptenceResponse.Data.ToList();

            foreach(var item in doctorItems)
            {
                if (!item.IsHealthy)
                {
                    Reviews.Add(new History()
                    {
                        Date = item.Date,
                        IsHealthy = item.IsHealthy,
                        IsHanded = false,
                        IsGiven = false,
                        IsAccepted = false,
                    });
                    continue;
                }
                var mechanicHandoverThisDay = mechanicHandoverItems.FirstOrDefault(m => m.Date.Date == item.Date.Date);
                if(mechanicHandoverThisDay.Status != StatusForDto.Pending && mechanicHandoverThisDay.Status != StatusForDto.Completed)
                {
                    Reviews.Add(new History()
                    {
                        Date = item.Date,
                        IsHealthy = item.IsHealthy,
                        IsHanded = false,
                        IsGiven = false,
                        IsAccepted = false,
                    });
                    continue;
                }

                var operatorReviewThisDay = operatorItems.FirstOrDefault(o => o.Date.Value.Date == item.Date.Date);
                if(operatorReviewThisDay.Status != StatusForDto.Pending && operatorReviewThisDay.Status != StatusForDto.Completed)
                {
                    Reviews.Add(new History()
                    {
                        Date = item.Date,
                        IsHealthy = item.IsHealthy,
                        IsHanded = true,
                        IsGiven = false,
                        IsAccepted = false,
                    });
                    continue;
                }

                var mechanicAcceptThisDay = mechanicAcceptence.FirstOrDefault(o => o.Date.Date == item.Date.Date);
                if(mechanicAcceptThisDay.Status != StatusForDto.Pending && mechanicAcceptThisDay.Status != StatusForDto.Completed)
                {
                    Reviews.Add(new History()
                    {
                        Date = item.Date,
                        IsHealthy = item.IsHealthy,
                        IsHanded = true,
                        IsGiven = true,
                        IsAccepted = false,
                    });
                    continue;
                }

                Reviews.Add(new History()
                {
                    Date = item.Date,
                    IsHealthy = true,
                    IsHanded = true,
                    IsGiven = true,
                    IsAccepted = true,
                });
            }
        } 
    }
}
