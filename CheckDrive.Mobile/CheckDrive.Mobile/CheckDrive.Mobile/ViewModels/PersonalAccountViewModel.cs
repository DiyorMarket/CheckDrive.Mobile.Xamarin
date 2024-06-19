using CheckDrive.ApiContracts.Driver;
using CheckDrive.Mobile.Services;
using CheckDrive.Mobile.Views;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CheckDrive.Mobile.ViewModels
{
    public class PersonalAccountViewModel : BaseViewModel
    {
        private DriverDto _driver;
        public DriverDto Driver {
            get => _driver;
            set
            {
               SetProperty(ref _driver, value);
               OnPropertyChanged(nameof(_driver));
            }
        }
        
        private string fullName;
        public string FullName
        {

            get => fullName;
            set => SetProperty(ref fullName, value);
        }
        private string _phone;
        public string PhoneNumber
        {

            get => _phone;
            set => SetProperty(ref _phone, value);
        }
        private string _login;
        public string Login
        {

            get => _login;
            set => SetProperty(ref _login, value);
        }

        public ICommand LogOutProfile { get; }

        public PersonalAccountViewModel()
        {
            LogOutProfile = new Command(NavigationLoginPage);
            InitializeDataAsync().ConfigureAwait(false);
        }

        private async Task InitializeDataAsync()
        {
            if(IsBusy) return;
            IsBusy = true;

            await Task.Run(() => {
                GetDriverData();    
            });

            IsBusy = false;
        }

        private void  GetDriverData()
        {
            _driver = DataService.GetAccount();
            FullName = $"{_driver.FirstName} {_driver.LastName}";
            Login = _driver.Login;
            PhoneNumber = _driver.PhoneNumber;
        }

        private void NavigationLoginPage()
        {
            DataService.RemoveAcoountData();
            Application.Current.MainPage = new LoginPage();
        }
    }
}
