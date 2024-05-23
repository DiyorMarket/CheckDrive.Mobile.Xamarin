using CheckDrive.Mobile.Helpers;
using CheckDrive.Web.Stores.DispatcherReviews;
using CheckDrive.Web.Stores.DoctorReviews;
using CheckDrive.Web.Stores.MechanicAcceptances;
using CheckDrive.Web.Stores.MechanicHandovers;
using CheckDrive.Web.Stores.OperatorReviews;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CheckDrive.Mobile.ViewModels
{
    public class HistoryViewModel
    {
        private readonly IDoctorReviewDataStore _doctorReviewDataStore;
        private readonly IMechanicHandoverDataStore _mechanicHandoverDataStore;
        private readonly IOperatorReviewDataStore _operatorReviewDataStore;
        private readonly IMechanicAcceptanceDataStore _mechanicAcceptanceDataStore;

        public ObservableCollection<History> Reviews { get; private set; }

        public HistoryViewModel(IDoctorReviewDataStore doctorReviewDataStore, IMechanicHandoverDataStore mechanicHandoverDataStore, IOperatorReviewDataStore operatorReviewDataStore, IMechanicAcceptanceDataStore mechanicAcceptanceDataStore)
        {
            _doctorReviewDataStore = doctorReviewDataStore;
            _mechanicHandoverDataStore = mechanicHandoverDataStore;
            _operatorReviewDataStore = operatorReviewDataStore;
            _mechanicAcceptanceDataStore = mechanicAcceptanceDataStore;

            Reviews = new ObservableCollection<History>();

            GetDispatcherReviews();
        }

        public void GetDispatcherReviews()
        {
            Reviews.Clear();

            var doctorItems = _doctorReviewDataStore.GetDoctorReviewsByDriverId(2).Data;
            var mechanicHandoverItems = _mechanicHandoverDataStore.GetMechanicHandoversByDriverId(2).Data;
            var operatorItems = _operatorReviewDataStore.GetOperatorReviewsByDriverId(2).Data;
            var mechanicAcceptence = _mechanicAcceptanceDataStore.GetMechanicAcceptancesByDriverId(2).Data;


            var historyItem = new History();
            foreach (var item in doctorItems)
            {
                historyItem.Date = item.Date;
                historyItem.IsHealthy = item.IsHealthy;
            }
            foreach (var item in mechanicHandoverItems)
            {
                historyItem.IsHanded = item.IsHanded;
            }
            foreach (var item in operatorItems)
            {
                historyItem.IsGiven = item.IsGiven;
            }
            foreach (var item in mechanicAcceptence)
            {
                historyItem.IsAccepted = item.IsAccepted;
            }
            Reviews.Add(historyItem);
        }
    }
}
