using CheckDrive.ApiContracts;
using CheckDrive.Web.Stores.DoctorReviews;
using CheckDrive.Web.Stores.MechanicAcceptances;
using CheckDrive.Web.Stores.MechanicHandovers;
using CheckDrive.Web.Stores.OperatorReviews;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CheckDrive.Mobile.ViewModels
{
    public class RoadMapViewModel : BaseViewModel
    {
        private readonly IDoctorReviewDataStore _doctorReviewDatastore;
        private readonly IMechanicAcceptanceDataStore _mechanicAcceptanceDataStore;
        private readonly IMechanicHandoverDataStore _mechanicHandoverDataStore;
        private readonly IOperatorReviewDataStore _operatorReviewDataStore;

        private StatusForDto _doctorStatus = StatusForDto.Pending;
        public StatusForDto DoctorStatusCheck
        {
            get { return _doctorStatus; }
            set
            {
                SetProperty(ref _doctorStatus, value);
                OnPropertyChanged(nameof(_doctorStatus));            }
        }

        private StatusForDto _mechanicAcceptanceStatus = StatusForDto.Pending;
        public StatusForDto MechanicAcceptanceStatusCheck
        {
            get { return _mechanicAcceptanceStatus; }
            set
            {
                SetProperty(ref _mechanicAcceptanceStatus, value);
                OnPropertyChanged(nameof(_mechanicAcceptanceStatus));
            }
        }

        private StatusForDto _operatorStatus = StatusForDto.Pending;
        public StatusForDto OperatorStatusCheck
        {
            get { return _operatorStatus; }
            set
            {
                SetProperty(ref _operatorStatus, value);
                OnPropertyChanged(nameof(_operatorStatus));
            }
        }

        private StatusForDto _mechanicHandoverStatus = StatusForDto.Pending;
        public StatusForDto MechanicHandoverStatusCheck
        {
            get { return _mechanicHandoverStatus; }
            set
            {
                SetProperty(ref _mechanicHandoverStatus, value);
                OnPropertyChanged(nameof(_mechanicHandoverStatus));
            }
        }

        private double oilPresentValue;
        public double OilPresentValue
        {
            get => oilPresentValue;
            set
            {
                if (oilPresentValue != value)
                {
                    oilPresentValue = value;
                    OnPropertyChanged(nameof(oilPresentValue));
                }
            }
        }

        public string OilValueToString { get; set; }

        private float oilPercent;
        public float OilPercent
        {
            get => oilPercent;
            set
            {
                if (oilPercent != value)
                {
                    oilPercent = value;
                    OnPropertyChanged(nameof(oilPercent));
                }
            }
        }
        private string message;

        public string Message
        {
            get => message;
            set
            {
                message = value;
                OnPropertyChanged();
            }
        }

        public string DoctorCheckTimeToString { get; set; } 
        public string MechanicAcceptenceCheckTime { get; set; }
        public string OperatorCheckTime { get; set; }
        public string MechanicHandoverCheckTime { get; set; }

        public DateTime StartDateForProgressBar { get; set; }
        public DateTime TodayDateForProgressBar { get; set; }
        public DateTime EndDateForProgressBar { get; set; }

        public RoadMapViewModel(IDoctorReviewDataStore doctorReviewDataStore, IMechanicAcceptanceDataStore mechanicAcceptanceDataStore, IOperatorReviewDataStore operatorReviewDataStore, IMechanicHandoverDataStore mechanicHandoverDataStore)
        {
            _doctorReviewDatastore = doctorReviewDataStore;
            _mechanicAcceptanceDataStore = mechanicAcceptanceDataStore;
            _operatorReviewDataStore = operatorReviewDataStore;
            _mechanicHandoverDataStore = mechanicHandoverDataStore;

            LoadViewPage();
        }
         public async void LoadViewPage()
        {
            IsBusy = true;
            await Task.Run(() => {
                GetOilResult();
                GetMessage();
                CheckDoctorStatusValue();
            });
            IsBusy = false;
        }

        private void GetOilResult()
        {
            GetDateForProgressBar();

            var driverhistoryForOil = _operatorReviewDataStore.GetOperatorReviews().Data.ToList().FindAll(x => x.DriverId == 1);
            foreach(var operatorReview in driverhistoryForOil)
            {
                if (operatorReview.Date >= StartDateForProgressBar 
                    && operatorReview.Date <= TodayDateForProgressBar
                    && operatorReview.Status == StatusForDto.Completed)
                {
                    oilPresentValue += operatorReview.OilAmount;
                }
            }
            OilValueToString = $"{oilPresentValue} L";
            oilPercent = (float)(OilPresentValue / 450);
        }

        private void GetMessage()
        {
            IsBusy = true;

            Message = "Siz davlat raqami 'P333MB' bo'lgan Malibu avtomobilini qabul qilasizmi";

            IsBusy = false;
        }

        private void CheckDoctorStatusValue()
        {
            var doctorReviews = _doctorReviewDatastore.GetDoctorReviews().Data;

            var doctorReview = doctorReviews.FirstOrDefault(x => x.DriverId == 1 && x.Date.Day == TodayDateForProgressBar.Day);

            if (doctorReview != null)
            {
                if (doctorReview.IsHealthy)
                {
                    _doctorStatus = StatusForDto.Completed;
                    CheckMechanicHandoverStatusValue();
                }
                else
                {
                    _doctorStatus = StatusForDto.Rejected;
                    _mechanicHandoverStatus = StatusForDto.Rejected;
                    _operatorStatus = StatusForDto.Rejected;
                    _mechanicAcceptanceStatus = StatusForDto.Rejected;
                }

                ChangedCheckTimeByStatus();
            }
        }

        private void CheckMechanicHandoverStatusValue()
        {
            var mechanicHandover = _mechanicHandoverDataStore.GetMechanicHandovers().Data.FirstOrDefault(x => x.DriverId == 1 && x.Date.Day == TodayDateForProgressBar.Day);
            
            if(mechanicHandover != null)
            {
                if (mechanicHandover.IsHanded)
                {
                    MechanicHandoverStatusCheck = StatusForDto.Completed;
                    CheckOperatorStatusValue();
                    return;
                }
                else
                {
                    MechanicHandoverStatusCheck = StatusForDto.Rejected;
                    OperatorStatusCheck = StatusForDto.Rejected;
                    MechanicAcceptanceStatusCheck = StatusForDto.Rejected;
                    return;
                }
            }

        }
        private void CheckOperatorStatusValue()
        {
            var operatorReview = _operatorReviewDataStore.GetOperatorReviews().Data.FirstOrDefault(x => x.DriverId == 1 && x.Date.Day == TodayDateForProgressBar.Day);
            
            if(operatorReview != null)
            {
                if (operatorReview.IsGiven)
                {
                    OperatorStatusCheck = StatusForDto.Completed;
                    CheckMechanicAcceptanceStatusValue();
                    return;
                }
                else
                {
                    OperatorStatusCheck = StatusForDto.Rejected;
                    MechanicAcceptanceStatusCheck = StatusForDto.Rejected;
                    return;
                }
            }

        }
        private void CheckMechanicAcceptanceStatusValue()
        {
            var mechanicAcceptance = _mechanicAcceptanceDataStore.GetMechanicAcceptances().Data.FirstOrDefault(x => x.DriverId == 1 && x.Date.Day == TodayDateForProgressBar.Day);

            if(mechanicAcceptance != null)
            {
                if (mechanicAcceptance.IsAccepted)
                {
                    MechanicAcceptanceStatusCheck = StatusForDto.Completed;
                }
                else
                {
                    MechanicAcceptanceStatusCheck = StatusForDto.Rejected;
                    return;
                }
            }

        }

        private void GetDateForProgressBar()
        {
            StartDateForProgressBar = DateTime.Now.Date.AddDays(-(DateTime.Now.Date.Day - 1));
            TodayDateForProgressBar = DateTime.Now.Date;
            EndDateForProgressBar = DateTime.Now.Date.AddMonths(+1).AddDays(-DateTime.Now.Date.Day);
        }

        private void ChangedCheckTimeByStatus()
        {
            ChangedDoctorCheckTime();
            ChangedMechanicHandoverCheckTime();
            ChangedOperatorCheckTime();
            ChangedMechanicAccCheckTime();
        }

        private void ChangedDoctorCheckTime()
        {
            if(_doctorStatus == StatusForDto.Completed
                 || _doctorStatus == StatusForDto.Rejected)
            {
                DoctorCheckTimeToString = DateTime.Now.ToString("HH : mm");
                return;
            }

            DoctorCheckTimeToString = "";
        }
        private void ChangedMechanicAccCheckTime()
        {
            if(_mechanicAcceptanceStatus == StatusForDto.Completed
                 || _mechanicAcceptanceStatus == StatusForDto.Rejected)
            {
                MechanicAcceptenceCheckTime = DateTime.Now.ToString("HH : mm");
                return;
            }

            MechanicAcceptenceCheckTime = "";
        }
        private void ChangedOperatorCheckTime()
        {
            if(_operatorStatus == StatusForDto.Completed
                 || _operatorStatus == StatusForDto.Rejected)
            {
                OperatorCheckTime = DateTime.Now.ToString("HH : mm");
                return;
            }

            OperatorCheckTime = "";
        }
        private void ChangedMechanicHandoverCheckTime()
        {
            if(_mechanicHandoverStatus == StatusForDto.Completed
                 || _mechanicHandoverStatus == StatusForDto.Rejected)
            {
                MechanicHandoverCheckTime = DateTime.Now.ToString("HH : mm");
                return;
            }

            MechanicHandoverCheckTime = "";
        }
    }
}
