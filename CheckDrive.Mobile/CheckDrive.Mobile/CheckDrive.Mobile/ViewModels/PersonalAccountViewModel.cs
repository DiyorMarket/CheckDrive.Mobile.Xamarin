using CheckDrive.ApiContracts.Driver;
using CheckDrive.Mobile.Services;
using CheckDrive.Mobile.Views;
using System.Windows.Input;
using Xamarin.Forms;

namespace CheckDrive.Mobile.ViewModels
{
    public class PersonalAccountViewModel : BaseViewModel
    {
        public DriverDto Driver { get; set; }
        public string FullName {  get; set; }

        public ICommand LogOutProfile { get; }

        public PersonalAccountViewModel()
        {
            LogOutProfile = new Command(NavigationLoginPage);
            GetDriverData();
        }

        public void GetDriverData()
        {
            Driver = DataService.GetAccount();
            FullName = Driver.FirstName + " " + Driver.LastName;
        }

        private void NavigationLoginPage()
        {
             DataService.RemoveAcoountData();
             Application.Current.MainPage = new LoginPage();
        }
    }
}
