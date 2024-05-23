using CheckDrive.ApiContracts.Driver;
using CheckDrive.Mobile.Views;
using CheckDrive.Web.Stores.Drivers;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace CheckDrive.Mobile.ViewModels
{
    public class PersonalAccountViewModel : BaseViewModel
    {
        private readonly IDriverDataStore _driverDataStore;
        public DriverDto Driver { get; set; }

        public ICommand RegisterCommand { get; }

        public PersonalAccountViewModel(IDriverDataStore driverDataStore)
        {
            RegisterCommand = new Command(NavigationLoginPage);
            _driverDataStore = driverDataStore;
            GetDriverData();
        }

        public void GetDriverData()
        {
            var drivers  = _driverDataStore.GetDrivers().Data.ToList();
            Driver = drivers[0];
        }

        private void NavigationLoginPage()
        {
             Application.Current.MainPage = new AppShell();
        }
    }
}
