using CheckDrive.ApiContracts.MechanicHandover;
using CheckDrive.Mobile.Helpers;
using CheckDrive.Web.Stores.DoctorReviews;
using CheckDrive.Web.Stores.MechanicAcceptances;
using CheckDrive.Web.Stores.MechanicHandovers;
using CheckDrive.Web.Stores.OperatorReviews;
using System;
using System.Collections.ObjectModel;
using System.Linq;

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

            int itemCount = Math.Min(Math.Min(doctorItems.Count(), mechanicHandoverItems.Count()), Math.Min(operatorItems.Count(), mechanicAcceptence.Count()));

            for (int i = 0; i < itemCount; i++)
            {
                var historyItem = new History
                {
                    Date = doctorItems.ToList()[i].Date,
                    IsHealthy = doctorItems.ToList()[i].IsHealthy,
                    IsHanded = mechanicHandoverItems.ToList()[i].IsHanded,
                    IsGiven = operatorItems.ToList()[i].IsGiven,
                    IsAccepted = mechanicAcceptence.ToList()[i].IsAccepted
                };

                Reviews.Add(historyItem);
            }
        }
    }
}
