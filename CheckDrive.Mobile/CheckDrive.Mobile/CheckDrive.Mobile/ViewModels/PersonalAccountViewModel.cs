using CheckDrive.Mobile.Services;
using CheckDrive.Mobile.Views;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CheckDrive.Mobile.ViewModels
{
    public class PersonalAccountViewModel : BaseViewModel
    {
        public ICommand LogOutProfile { get; }

        private string _fullName;
        public string FullName
        {

            get => _fullName;
            set => SetProperty(ref _fullName, value);
        }
        private string _phoneNumber;
        public string PhoneNumber
        {

            get => _phoneNumber;
            set => SetProperty(ref _phoneNumber, value);
        }
        private string _login;
        public string Login
        {

            get => _login;
            set => SetProperty(ref _login, value);
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

        public PersonalAccountViewModel( )
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
            var _driver = DataService.GetAccount();
            FullName = $"{_driver.FirstName} {_driver.LastName}";
            PhoneNumber = _driver.PhoneNumber;
            Login = _driver.Login;
        }

        private void NavigationLoginPage()
        {
            DataService.RemoveAllAcoountData();
            Application.Current.MainPage = new LoginPage();
        }
    }
}
