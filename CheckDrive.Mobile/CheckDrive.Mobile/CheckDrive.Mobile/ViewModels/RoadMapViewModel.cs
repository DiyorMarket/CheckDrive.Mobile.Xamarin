using CheckDrive.Mobile.Models;

namespace CheckDrive.Mobile.ViewModels
{
    public class RoadMapViewModel : BaseViewModel
    {
        private Status _doctorStatus;
        public Status DoctorStatusCheck
        {
            get { return _doctorStatus; }
            set
            {
                SetProperty(ref _doctorStatus, value);
                OnPropertyChanged(nameof(_doctorStatus));
            }
        }

        private Status _mechanicAcceptanceStatus;
        public Status MechanicAcceptanceStatusCheck
        {
            get { return _mechanicAcceptanceStatus; }
            set
            {
                SetProperty(ref _mechanicAcceptanceStatus, value);
                OnPropertyChanged(nameof(_mechanicAcceptanceStatus));
            }
        }

        private Status _operatorStatus;
        public Status OperatorStatusCheck
        {
            get { return _operatorStatus; }
            set
            {
                SetProperty(ref _operatorStatus, value);
                OnPropertyChanged(nameof(_operatorStatus));
            }
        }

        private Status _mechanicHandoverStatus;
        public Status MechanicHandoverStatusCheck
        {
            get { return _mechanicHandoverStatus; }
            set
            {
                SetProperty(ref _mechanicHandoverStatus, value);
                OnPropertyChanged(nameof(_mechanicHandoverStatus));
            }
        }

        private double oilPrecentValue;
        public double OilPrecentValue
        {
            get => oilPrecentValue;
            set
            {
                if (oilPrecentValue != value)
                {
                    oilPrecentValue = value;
                    OnPropertyChanged();
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
                    OnPropertyChanged();
                }
            }
        }

        public RoadMapViewModel()
        {
            GetOilResult();
            GetStatusValue();
        }

        public void GetOilResult()
        {
            oilPrecentValue = 45;
            OilValueToString = $"{oilPrecentValue} L";
            oilPercent = (float)(OilPrecentValue / 450);
        }

        public void GetStatusValue()
        {
            _doctorStatus = Status.Completed;
            _mechanicAcceptanceStatus = Status.Completed;
            _operatorStatus = Status.Rejected;
            _mechanicHandoverStatus = Status.Pending;
        }
    }
}
