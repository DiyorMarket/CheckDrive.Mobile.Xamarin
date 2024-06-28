using CheckDrive.ApiContracts;
using CheckDrive.ApiContracts.DoctorReview;
using CheckDrive.ApiContracts.Driver;
using CheckDrive.ApiContracts.MechanicAcceptance;
using CheckDrive.ApiContracts.MechanicHandover;
using CheckDrive.ApiContracts.OperatorReview;
using CheckDrive.Mobile.Services;
using CheckDrive.Mobile.Views;
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
                OnPropertyChanged(nameof(DoctorStatusCheck));
            }
        }

        private StatusForDto _mechanicAcceptanceStatus;
        public StatusForDto MechanicAcceptanceStatusCheck
        {
            get { return _mechanicAcceptanceStatus; }
            set
            {
                SetProperty(ref _mechanicAcceptanceStatus, value);
                OnPropertyChanged(nameof(MechanicAcceptanceStatusCheck));
            }
        }

        private StatusForDto _operatorStatus;
        public StatusForDto OperatorStatusCheck
        {
            get { return _operatorStatus; }
            set
            {
                SetProperty(ref _operatorStatus, value);
                OnPropertyChanged(nameof(OperatorStatusCheck));
            }
        }

        private StatusForDto _mechanicHandoverStatus;
        public StatusForDto MechanicHandoverStatusCheck
        {
            get { return _mechanicHandoverStatus; }
            set
            {
                SetProperty(ref _mechanicHandoverStatus, value);
                OnPropertyChanged(nameof(MechanicHandoverStatusCheck));
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
                    OnPropertyChanged(nameof(OilPresentValue));
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
                    OnPropertyChanged(nameof(OilPercent));
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

        private string _doctorCheckTime = "";
        public string DoctorCheckTime
        {
            get => _doctorCheckTime;
            set
            {
                if (_doctorCheckTime != value)
                {
                    _doctorCheckTime = value;
                    OnPropertyChanged(nameof(DoctorCheckTime));
                }
            }
        }

        private string _mechanicHandoverCheckTime = "";
        public string MechanicHandoverCheckTime
        {
            get => _mechanicHandoverCheckTime;
            set
            {
                if (_mechanicHandoverCheckTime != value)
                {
                    _mechanicHandoverCheckTime = value;
                    OnPropertyChanged(nameof(MechanicHandoverCheckTime));
                }
            }
        }

        private string _operatorCheckTime = "";
        public string OperatorCheckTime
        {
            get => _operatorCheckTime;
            set
            {
                if (_operatorCheckTime != value)
                {
                    _operatorCheckTime = value;
                    OnPropertyChanged(nameof(OperatorCheckTime));
                }
            }
        }

        private string _mechanicAcceptanceCheckTime = "";
        public string MechanicAcceptanceCheckTime
        {
            get => _mechanicAcceptanceCheckTime;
            set
            {
                if (_mechanicAcceptanceCheckTime != value)
                {
                    _mechanicAcceptanceCheckTime = value;
                    OnPropertyChanged(nameof(MechanicAcceptanceCheckTime));
                }
            }
        }

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
            AcceptButtonCommand = new Command(async () => await AcceptButton());
            RejectButtonCommand = new Command(async () => await RejectButton());

            LoadViewPage();
        }

        public async void LoadViewPage()
        {
            IsBusy = true;
            await GetOilResult();
            await CheckDoctorStatusValue();
            await CheckNotification();
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

            await Task.WhenAll();
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
            var doctorReviewsResponse = await _doctorReviewDatastore.GetDoctorReviewsAsync(TodayDateForProgressBar);
            var doctorReviews = doctorReviewsResponse.Data;

            var doctorReview = doctorReviews.FirstOrDefault(x => x.DriverId == _driver.Id);

            if (doctorReview != null)
            {
                if (doctorReview.IsHealthy)
                {
                    DoctorStatusCheck = StatusForDto.Completed;
                    ChangedDoctorCheckTime(doctorReview);
                    await CheckMechanicHandoverStatusValue();
                    return;
                }

                DoctorStatusCheck = StatusForDto.Rejected;
                MechanicHandoverStatusCheck = StatusForDto.Rejected;
                OperatorStatusCheck = StatusForDto.Rejected;
                MechanicAcceptanceStatusCheck = StatusForDto.Rejected;
                ChangedDoctorCheckTime(doctorReview);
            }
        }

        private async Task CheckMechanicHandoverStatusValue()
        {
            var mechanicHandovers = await _mechanicHandoverDataStore.GetMechanicHandoversAsync(TodayDateForProgressBar);
            var mechanicHandover = mechanicHandovers.Data.FirstOrDefault(x => x.DriverId == _driver.Id);

            if (mechanicHandover != null)
            {
                if (mechanicHandover.IsHanded && mechanicHandover.Status == StatusForDto.Completed)
                {
                    MechanicHandoverStatusCheck = StatusForDto.Completed;
                    ChangedMechanicHandoverCheckTime(mechanicHandover);
                    await CheckOperatorStatusValue();
                    return;
                }

                MechanicHandoverStatusCheck = StatusForDto.Rejected;
                OperatorStatusCheck = StatusForDto.Rejected;
                MechanicAcceptanceStatusCheck = StatusForDto.Rejected;
                ChangedMechanicHandoverCheckTime(mechanicHandover);
            }
        }

        private async Task CheckOperatorStatusValue()
        {
            var operatorReviewResponse = await _operatorReviewDataStore.GetOperatorReviewsAsync(TodayDateForProgressBar);
            var operatorReview = operatorReviewResponse.Data.FirstOrDefault(x => x.DriverId == _driver.Id);

            if (operatorReview != null)
            {
                if (operatorReview.IsGiven && operatorReview.Status == StatusForDto.Completed)
                {
                    OperatorStatusCheck = StatusForDto.Completed;
                    ChangedOperatorCheckTime(operatorReview);
                    await CheckMechanicAcceptanceStatusValue();
                    return;
                }

                OperatorStatusCheck = StatusForDto.Rejected;
                MechanicAcceptanceStatusCheck = StatusForDto.Rejected;
                ChangedOperatorCheckTime(operatorReview);
            }
        }

        private async Task CheckMechanicAcceptanceStatusValue()
        {
            var mechanicAcceptanceResponse = await _mechanicAcceptanceDataStore.GetMechanicAcceptancesAsync(TodayDateForProgressBar);
            var mechanicAcceptance = mechanicAcceptanceResponse.Data.FirstOrDefault(x => x.DriverId == _driver.Id);

            if (mechanicAcceptance != null)
            {
                if (mechanicAcceptance.IsAccepted && mechanicAcceptance.Status == StatusForDto.Completed)
                {
                    MechanicAcceptanceStatusCheck = StatusForDto.Completed;
                    ChangedMechanicAcceptanceCheckTime(mechanicAcceptance);
                    return;
                }

                MechanicAcceptanceStatusCheck = StatusForDto.Rejected;
                ChangedMechanicAcceptanceCheckTime(mechanicAcceptance);
            }
        }

        #endregion

        #region Departments status check time methods

        private void ChangedDoctorCheckTime(DoctorReviewDto reviewDto)
        {
            DateTime utcDateTime = reviewDto.Date;
            TimeZoneInfo localTimeZone = TimeZoneInfo.FindSystemTimeZoneById("GMT+5");
            DateTime localDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, localTimeZone);
            DoctorCheckTime = localDateTime.ToString("HH : mm");
        }

        private void ChangedMechanicAcceptanceCheckTime(MechanicAcceptanceDto acceptanceDto)
        {
            DateTime utcDateTime = acceptanceDto.Date;
            TimeZoneInfo localTimeZone = TimeZoneInfo.FindSystemTimeZoneById("GMT+5");
            DateTime localDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, localTimeZone);
            MechanicAcceptanceCheckTime = localDateTime.ToString("HH : mm");
        }

        private void ChangedOperatorCheckTime(OperatorReviewDto reviewDto)
        {
            DateTime utcDateTime = reviewDto.Date.Value;
            TimeZoneInfo localTimeZone = TimeZoneInfo.FindSystemTimeZoneById("GMT+5");
            DateTime localDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, localTimeZone);
            OperatorCheckTime = localDateTime.ToString("HH : mm");
        }

        private void ChangedMechanicHandoverCheckTime(MechanicHandoverDto handoverDto)
        {
            DateTime utcDateTime = handoverDto.Date;
            TimeZoneInfo localTimeZone = TimeZoneInfo.FindSystemTimeZoneById("GMT+5");
            DateTime localDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, localTimeZone);
            MechanicHandoverCheckTime = localDateTime.ToString("HH : mm");
        }

        #endregion

        #region Notification methods

        private async Task AcceptButton()
        {
            var statusNumber = DataService.GetSignalRData().statusNumber;

            switch (statusNumber)
            {
                case 0:
                    MechanicHandoverStatusCheck = StatusForDto.Completed;
                    break;
                case 1:
                    OperatorStatusCheck = StatusForDto.Completed;
                    break;
                case 2:
                    MechanicAcceptanceStatusCheck = StatusForDto.Completed;
                    break;
            }

            await _signalRService.SendResponse(true);
            await ClosePopup();
            await RefreshPage();
        }

        private async Task RejectButton()
        {
            var statusNumber = DataService.GetSignalRData().statusNumber;

            switch (statusNumber)
            {
                case 0:
                    MechanicHandoverStatusCheck = StatusForDto.Rejected;
                    break;
                case 1:
                    OperatorStatusCheck = StatusForDto.Rejected;
                    break;
                case 2:
                    MechanicAcceptanceStatusCheck = StatusForDto.Rejected;
                    break;
            }

            await _signalRService.SendResponse(false);
            await ClosePopup();
            await RefreshPage();
        }

        private async Task ClosePopup()
        {
            await PopupNavigation.Instance.PopAsync(true);
        }

        private async Task RefreshPage()
        {
            await App.Current.MainPage.Navigation.PushAsync(new RoadMapPage());
        }

        #endregion

        private async Task CheckStatusForBeforeDay()
        {
            var mechanicAccepDS = await _mechanicAcceptanceDataStore.GetMechanicAcceptancesAsync(_driver.Id, "dateDesc");
            var mechanicAccep = mechanicAccepDS.Data.FirstOrDefault();

            if (mechanicAccep != null && mechanicAccep.Status == StatusForDto.Pending)
            {
                DoctorStatusCheck = StatusForDto.Pending;
                MechanicHandoverStatusCheck = StatusForDto.Pending;
                OperatorStatusCheck = StatusForDto.Pending;
                MechanicAcceptanceStatusCheck = StatusForDto.Pending;
            }
        }
    }
}
