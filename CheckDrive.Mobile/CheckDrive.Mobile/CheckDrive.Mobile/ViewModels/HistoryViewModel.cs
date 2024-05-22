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

        public HistoryViewModel(IDispatcherReviewDataStore dispatcherReviewDataStore, IDoctorReviewDataStore doctorReviewDataStore)
        {
            _dispatcherDataStore = dispatcherReviewDataStore;
            _doctorReviewDataStore = doctorReviewDataStore;

            DispatcherReviews = new ObservableCollection<History>();

            GetDispatcherReviews();
        }

        public void GetDispatcherReviews()
        {
            DispatcherReviews.Clear();

            var dipatcherItems =  _dispatcherDataStore.GetDispatcherReviewsAsync().Result;
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
        }
    }
}
