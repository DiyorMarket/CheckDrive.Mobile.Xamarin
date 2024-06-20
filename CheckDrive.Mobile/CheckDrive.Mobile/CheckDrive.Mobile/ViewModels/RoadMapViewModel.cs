using CheckDrive.ApiContracts;
using CheckDrive.ApiContracts.Driver;
using CheckDrive.Mobile.Services;
using CheckDrive.Web.Stores.DoctorReviews;
using CheckDrive.Web.Stores.MechanicAcceptances;
using CheckDrive.Web.Stores.MechanicHandovers;
using CheckDrive.Web.Stores.OperatorReviews;
using Rg.Plugins.Popup.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CheckDrive.Mobile.ViewModels
{
    public class RoadMapViewModel : BaseViewModel
    {
        private readonly IDoctorReviewDataStore _doctorReviewDatastore;
        private readonly IMechanicAcceptanceDataStore _mechanicAcceptanceDataStore;
        private readonly IMechanicHandoverDataStore _mechanicHandoverDataStore;
        private readonly IOperatorReviewDataStore _operatorReviewDataStore;
        
        public ICommand AcceptButtonCommand { get; set; }
        public ICommand RejectButtonCommand { get; set; }
        
        public DateTime StartDateForProgressBar { get; set; }
        public DateTime TodayDateForProgressBar { get; set; }
        public DateTime EndDateForProgressBar { get; set; }
        private DriverDto _driver;
        private readonly SignalRService _signalRService;

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

        private double _oilPresentValue;
        public double OilPresentValue
        {
            get => _oilPresentValue;
            set
            {
                if (_oilPresentValue != value)
                {
                    _oilPresentValue = value;
                    OnPropertyChanged(nameof(_oilPresentValue));
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
        private string _doctorCheckTime;
        public string DoctorCheckTime
        {
            get => _doctorCheckTime;
            set
            {
                if (_doctorCheckTime != value)
                {
                    _doctorCheckTime = value;
                    OnPropertyChanged(nameof(_doctorCheckTime));
                }
            }
        }
        public string MechanicAcceptenceCheckTime { get; set; }
        public string OperatorCheckTime { get; set; }
        public string MechanicHandoverCheckTime { get; set; }

        public RoadMapViewModel(IDoctorReviewDataStore doctorReviewDataStore,
            IMechanicAcceptanceDataStore mechanicAcceptanceDataStore,
            IOperatorReviewDataStore operatorReviewDataStore, 
            IMechanicHandoverDataStore mechanicHandoverDataStore)
        {
            _doctorReviewDatastore = doctorReviewDataStore;
            _mechanicAcceptanceDataStore = mechanicAcceptanceDataStore;
            _operatorReviewDataStore = operatorReviewDataStore;
            _mechanicHandoverDataStore = mechanicHandoverDataStore;
            _driver = DataService.GetAccount();
            _signalRService = new SignalRService();
            AcceptButtonCommand = new Command(AcceptButton);
            RejectButtonCommand = new Command(RejectButton);

            LoadViewPage();
        }


        public void LoadViewPage()
         {
            IsBusy = true;
              GetOilResult();
              CheckDoctorStatusValue();
              CheckNotification();
            IsBusy = false;
         }

        private async Task GetOilResult()
        {
            GetDateForProgressBar();
            try
            {
                var operatorReviewResponse = await _operatorReviewDataStore.GetOperatorReviewsByDriverIdAsync(_driver.Id);
                var driverhistoryForOil = operatorReviewResponse.Data.ToList();

                foreach (var operatorReview in driverhistoryForOil)
                {
                    if (operatorReview.Date >= StartDateForProgressBar
                        && operatorReview.Date <= DateTime.Now)
                    {
                        _oilPresentValue += operatorReview.OilAmount;
                    }
                }
                OilValueToString = $"{_oilPresentValue} L";
                OilPercent = (float)(OilPresentValue / 450);
            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while retrieving fuel data.", ex);
            }
            
        }
        private void GetDateForProgressBar()
        {
            StartDateForProgressBar = DateTime.Now.Date.AddDays(-(DateTime.Now.Date.Day - 1));
            TodayDateForProgressBar = DateTime.Now.Date;
            EndDateForProgressBar = DateTime.Now.Date.AddMonths(+1).AddDays(-DateTime.Now.Date.Day);
        }
        private async Task CheckNotification()
        {
            await _signalRService.StartConnectionAsync();
        }

        #region Departments check status value methods
        private async Task CheckDoctorStatusValue()
        {
            var doctorReviewsResponse = await _doctorReviewDatastore.GetDoctorReviewsAsync(DateTime.Now);
            var doctorReviews = doctorReviewsResponse.Data;

            var doctorReview = doctorReviews.FirstOrDefault(x => x.DriverId == _driver.Id);

            if (doctorReview != null)
            {
                if (doctorReview.IsHealthy)
                {
                    DoctorStatusCheck = StatusForDto.Completed;
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
        private async void CheckMechanicHandoverStatusValue()
        {
            var mechanicHandovers = await _mechanicHandoverDataStore.GetMechanicHandoversAsync(DateTime.Now);
            var mechanicHandover = mechanicHandovers.Data.FirstOrDefault(x => x.DriverId == _driver.Id);

            if (mechanicHandover != null)
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
        private async void CheckOperatorStatusValue()
        {
            var operatorReviewResponse = await _operatorReviewDataStore
                .GetOperatorReviewsAsync(TodayDateForProgressBar);

            var operatorReview = operatorReviewResponse.Data
                .FirstOrDefault(x => x.DriverId == _driver.Id);

            if (operatorReview != null)
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
        private async void CheckMechanicAcceptanceStatusValue()
        {
            var mechanicAcceptanceResponse = await _mechanicAcceptanceDataStore
                .GetMechanicAcceptancesAsync(TodayDateForProgressBar);

            var mechanicAcceptance = mechanicAcceptanceResponse
                .Data.FirstOrDefault(x => x.DriverId == _driver.Id);

            if (mechanicAcceptance != null)
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
        #endregion

        #region Departments status check time methods
        private void ChangedCheckTimeByStatus()
        {
            ChangedDoctorCheckTime();
            ChangedMechanicHandoverCheckTime();
            ChangedOperatorCheckTime();
            ChangedMechanicAccCheckTime();
        }
        private void ChangedDoctorCheckTime()
        {
            if (DoctorStatusCheck == StatusForDto.Completed
                 || DoctorStatusCheck == StatusForDto.Rejected)
            {
                DoctorCheckTime = DateTime.Now.ToString("HH : mm");
                return;
            }

            DoctorCheckTime = "";
        }
        private void ChangedMechanicAccCheckTime()
        {
            if (_mechanicAcceptanceStatus == StatusForDto.Completed
                 || _mechanicAcceptanceStatus == StatusForDto.Rejected)
            {
                MechanicAcceptenceCheckTime = DateTime.Now.ToString("HH : mm");
                return;
            }

            MechanicAcceptenceCheckTime = "";
        }
        private void ChangedOperatorCheckTime()
        {
            if (_operatorStatus == StatusForDto.Completed
                 || _operatorStatus == StatusForDto.Rejected)
            {
                OperatorCheckTime = DateTime.Now.ToString("HH : mm");
                return;
            }

            OperatorCheckTime = "";
        }
        private void ChangedMechanicHandoverCheckTime()
        {
            if (_mechanicHandoverStatus == StatusForDto.Completed
                 || _mechanicHandoverStatus == StatusForDto.Rejected)
            {
                MechanicHandoverCheckTime = DateTime.Now.ToString("HH : mm");
                return;
            }

            MechanicHandoverCheckTime = "";
        }
        #endregion

        #region Notification methods
        private async void AcceptButton()
        {
            await _signalRService.SendResponse(true);
            ClosePopup();
        }
        private async void RejectButton()
        {
            await _signalRService.SendResponse(false);
            ClosePopup();
        }
        private async void ClosePopup()
        {
            await PopupNavigation.Instance.PopAsync(true);
        }
        #endregion

        private async Task CheckStatusForBeforeDay()
        {
            var mechanicAccepDS = await _mechanicAcceptanceDataStore.GetMechanicAcceptancesAsync(_driver.Id, "dateDesc");
            var mechanicAccep = mechanicAccepDS.Data.First();

            if (mechanicAccep != null && mechanicAccep.Status == StatusForDto.Pending)
            {
                _doctorStatus = StatusForDto.Pending;
                _mechanicHandoverStatus = StatusForDto.Pending;
                _operatorStatus = StatusForDto.Pending;
                _mechanicAcceptanceStatus = StatusForDto.Pending;
            }

        }

    }
}
