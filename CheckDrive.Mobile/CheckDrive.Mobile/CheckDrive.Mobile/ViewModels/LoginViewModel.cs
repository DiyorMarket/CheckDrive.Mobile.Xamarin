using CheckDrive.ApiContracts.Account;
using CheckDrive.ApiContracts.Driver;
using CheckDrive.Mobile.Services;
using CheckDrive.Mobile.Stores.Drivers;
using CheckDrive.Web.Stores.Accounts;
using CheckDrive.Web.Stores.Drivers;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace CheckDrive.Mobile.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IAccountDataStore _accountDataStore;
        private readonly IDriverDataStore _driverDataStore;

        private DriverDto Account {  get; set; }

        private string _login;
        public string Login
        {
            get { return _login; }
            set { SetProperty(ref _login, value); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        private bool _isPasswordVisible;
        public bool IsPasswordVisible
        {
            get => _isPasswordVisible;
            set
            {
                _isPasswordVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _isLoginVisible;
        public bool IsLoginVisible
        {
            get => _isLoginVisible;
            set
            {
                _isLoginVisible = value;
                OnPropertyChanged();
            }
        }

        public ICommand TogglePasswordVisibilityCommand { get; }
        public ICommand ToggleLoginVisibilityCommand { get; }

        public Command LoginCommand { get; }

        public LoginViewModel(IAccountDataStore accountDataStore, IDriverDataStore driverDataStore)
        {
            _accountDataStore = accountDataStore;
            _driverDataStore = driverDataStore;
            LoginCommand = new Command(OnLoginClicked);
            TogglePasswordVisibilityCommand = new Command(TogglePasswordVisibility);
            ToggleLoginVisibilityCommand = new Command(ToggleLoginVisibility);
        }

        private void OnLoginClicked(object obj)
        {
            if (string.IsNullOrWhiteSpace(Login))
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                return;
            }

            try
            {
                IsBusy = true;

                var isSuccess = CheckingDriverLogin();

                if (!isSuccess)
                {
                    Application.Current.MainPage.DisplayAlert("Login Failed", "Please check your credentials and try again.", "OK");

                    return;
                }

                if(Account.Password != Password) 
                {
                    Application.Current.MainPage.DisplayAlert("Password Failed", "Please check your credentials and try again.", "OK");

                    return;
                }

                Application.Current.MainPage = new AppShell();
            }
            catch
            {
                Application.Current.MainPage.DisplayAlert("Login Failed", "Please check your credentials and try again.", "OK");
            }
        }

        private bool CheckingDriverLogin()
        {
            var accounts = _accountDataStore.GetAccounts(Login).Data.ToList();
            var account = accounts[0];

            var driver = _driverDataStore.GetDrivers(account.Id).Data.ToList().First();

            if (driver != null)
            {
                Account = driver;
                DataService.SaveAccount(driver);
                return true;
            }

            return false;
        }

        private void TogglePasswordVisibility()
        {
            IsPasswordVisible = !IsPasswordVisible;
        }

        private void ToggleLoginVisibility()
        {
            IsLoginVisible = !IsLoginVisible;
        }
    }
}
