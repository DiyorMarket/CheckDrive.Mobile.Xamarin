using CheckDrive.Mobile.Helpers;
using CheckDrive.Web.Stores.DispatcherReviews;
using CheckDrive.Web.Stores.DoctorReviews;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CheckDrive.Mobile.ViewModels
{
    public class HistoryViewModel : BaseViewModel
    {
        private readonly IDispatcherReviewDataStore _dispatcherDataStore;
        private readonly IDoctorReviewDataStore _doctorReviewDataStore;

        public ObservableCollection<History> DispatcherReviews { get; private set; }

        public HistoryViewModel(IDispatcherReviewDataStore dispatcherReviewDataStore, IDoctorReviewDataStore doctorReviewDataStore)
        {
            _dispatcherDataStore = dispatcherReviewDataStore;
            _doctorReviewDataStore = doctorReviewDataStore;

            DispatcherReviews = new ObservableCollection<History>();

            LoadViewPage();
        }

        public async void LoadViewPage()
        {

            IsBusy = true;
            await Task.Run(() => {
                GetDispatcherReviews();
            });
            IsBusy = false;
        }

        public void GetDispatcherReviews()
        {
            IsBusy = true;
            
            DispatcherReviews.Clear();

            var dipatcherItems =  _dispatcherDataStore.GetDispatcherReviews().Data;
            var doctorItems =  _doctorReviewDataStore.GetDoctorReviews().Data;


            foreach (var item in dipatcherItems)
            {
                if (item.DriverId == 2)
                {
                    var historyItem = new History
                    {
                        Date = item.Date,
                    };
                    foreach (var doctoritem in doctorItems)
                    {
                        historyItem.IsHealthy = doctoritem.IsHealthy;
                    }
                    DispatcherReviews.Add(historyItem);
                }
            }

            IsBusy = false;
        }
    }
}
