using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CheckDrive.Mobile.ViewModels
{
    public class RoadMapViewModel : INotifyPropertyChanged
    {
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public RoadMapViewModel()
        {
            GetOilResult();
        }

        public void GetOilResult()
        {
            oilPrecentValue = 45;
            OilValueToString = $"{oilPrecentValue} L";
            oilPercent = (float)(OilPrecentValue / 450);
        }
    }
}
