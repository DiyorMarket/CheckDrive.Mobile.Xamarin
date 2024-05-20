using CheckDrive.ApiContracts;
using CheckDrive.Web.Stores.DoctorReviews;
using CheckDrive.Web.Stores.MechanicAcceptances;
using CheckDrive.Web.Stores.MechanicHandovers;
using CheckDrive.Web.Stores.OperatorReviews;
using System;

namespace CheckDrive.Mobile.ViewModels
{
    public class RoadMapViewModel : BaseViewModel
    {
        private readonly IDoctorReviewDataStore _doctorReviewDatastore;
        private readonly IMechanicAcceptanceDataStore _mechanicAcceptanceDataStore;
        private readonly IMechanicHandoverDataStore _mechanicHandoverDataStore;
        private readonly IOperatorReviewDataStore _operatorReviewDataStore;

        private StatusForDto _doctorStatus;
        public StatusForDto DoctorStatusCheck
        {
            get { return _doctorStatus; }
            set
            {
                SetProperty(ref _doctorStatus, value);
                OnPropertyChanged(nameof(_doctorStatus));            }
        }

        private StatusForDto _mechanicAcceptanceStatus;
        public StatusForDto MechanicAcceptanceStatusCheck
        {
            get { return _mechanicAcceptanceStatus; }
            set
            {
                SetProperty(ref _mechanicAcceptanceStatus, value);
                OnPropertyChanged(nameof(_mechanicAcceptanceStatus));
            }
        }

        private StatusForDto _operatorStatus;
        public StatusForDto OperatorStatusCheck
        {
            get { return _operatorStatus; }
            set
            {
                SetProperty(ref _operatorStatus, value);
                OnPropertyChanged(nameof(_operatorStatus));
            }
        }

        private StatusForDto _mechanicHandoverStatus;
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

        public RoadMapViewModel()
        {
            _doctorReviewDatastore = new MockDoctorReviewDataStore();
            _mechanicAcceptanceDataStore = new MockMechanicAcceptanceDataStore();
            _operatorReviewDataStore = new MockOperatorReviewDataStore();
            _mechanicHandoverDataStore = new MockMechanicHandoverDataStore();

            GetOilResult();
            GetMessage();
            GetStatusValue();
        }

        public void GetOilResult()
        {
            GetDateForProgressBar();

            var driverhistoryForOil = _operatorReviewDataStore.GetOperatorReviews().Result.FindAll(x => x.DriverId == 1);
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

        public void GetMessage()
        {
            Message = "Siz davlat raqami 'P333MB' bo'lgan Malibu avtomobilini qabul qilasizmi";
        }
        public void GetStatusValue()
        {
            #region Dto package o'zgarsa to'g'irlanadi ! 

            //_doctorStatus = _doctorReviewDatastore.GetDoctorReviews().Result.FirstOrDefault(x => x.DriverId == user.Id).Status;
            //_mechanicAcceptanceStatus = _mechanicAcceptanceDataStore.GetMechanicAcceptances().Result.FirstOrDefault(x => x.DriverId == user.Id).Status;
            //_operatorStatus = _operatorReviewDataStore.GetOperatorReviews.Result.FirstOrDefault(x => x.DriverId == user.Id).Status;
            //_mechanicHandoverStatus = _mechanicHandoverDataStore.GetMechanicHandovers().Result.FirstOrDefault(x => x.DriverId == user.Id).Status; 
            #endregion

            _doctorStatus = StatusForDto.Completed;
            ChangedDoctorCheckTime();
            _mechanicAcceptanceStatus = StatusForDto.Completed;
            ChangedMechanicAccCheckTime();
            _operatorStatus = StatusForDto.Rejected;
            ChangedOperatorCheckTime();
            _mechanicHandoverStatus = StatusForDto.Pending;
            ChangedMechanicHandoverCheckTime();
        }

        private void GetDateForProgressBar()
        {
            StartDateForProgressBar = DateTime.Now.Date.AddDays(-(DateTime.Now.Date.Day - 1));
            TodayDateForProgressBar = DateTime.Now.Date;
            EndDateForProgressBar = DateTime.Now.Date.AddMonths(+1).AddDays(-DateTime.Now.Date.Day);
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
