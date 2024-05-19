using CheckDrive.Mobile.Helpers;
using CheckDrive.Web.Stores.DispatcherReviews;
using CheckDrive.Web.Stores.DoctorReviews;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CheckDrive.Mobile.ViewModels
{
    public class HistoryViewModel
    {
        private readonly IDispatcherReviewDataStore _dispatcherDataStore;
        private readonly IDoctorReviewDataStore _doctorReviewDataStore;

        public ObservableCollection<History> DispatcherReviews { get; private set; }

        public HistoryViewModel(IDispatcherReviewDataStore dispatcherDataStore, IDoctorReviewDataStore doctorReviewDataStore)
        {
            _dispatcherDataStore = dispatcherDataStore;
            _doctorReviewDataStore = doctorReviewDataStore;

            DispatcherReviews = new ObservableCollection<History>();

            GetDispatcherReviews();
        }

        public async Task GetDispatcherReviews()
        {
            DispatcherReviews.Clear();

            var dipatcherItems = await _dispatcherDataStore.GetDispatcherReviews();
            var doctorItems = await _doctorReviewDataStore.GetDoctorReviews();


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
        }
    }
}
