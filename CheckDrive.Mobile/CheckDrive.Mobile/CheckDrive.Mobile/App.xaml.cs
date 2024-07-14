using CheckDrive.ApiContracts.Driver;
using CheckDrive.Mobile.Services;
using CheckDrive.Mobile.Stores.Accounts;
using CheckDrive.Mobile.Stores.Drivers;
using CheckDrive.Mobile.Views;
using Rg.Plugins.Popup.Services;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CheckDrive.Mobile
{
    public partial class App : Application
    {
        private readonly ApiClient _client = new ApiClient();
        public App()
        {
            InitializeComponent();

            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;

            TaskRunProject();
        }

        private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (e.NetworkAccess != NetworkAccess.Internet)
            {
                MainPage = new NoInternetPage();
            }
            else
            {
                MainPage = new AppShell();
            }
        }

        private async void TaskRunProject()
        {
            if (!ConnectivityService.IsConnected())
            {
                MainPage = new NoInternetPage();
                return;
            }

            var isChecked = await CheckloginDate();

            if (isChecked)
            {
                MainPage = new AppShell();
                await CheckOldNotification();
                return;
            }
            MainPage = new LoginPage();
        }

        private async Task<bool> CheckloginDate()
        {
            var creationDate = DataService.GetCreationDate();
            var driver = DataService.GetAccount();

            if (creationDate != null && driver != null)
            {
                var isToken =  await CheckTokenDate(driver);
                return isToken;
            }

            DataService.RemoveAllAcoountData();

            return false;
        }

        private async Task<bool> CheckTokenDate(DriverDto driver)
        {
            var creationTokenDate = DataService.GetTokenCreationDate();
            var summHours = DateTime.Now - creationTokenDate;

            if (summHours.TotalHours >= 12)
            {
                var accaountDS = new AccountDataStore(_client);

                var token = await accaountDS.CreateTokenAsync(driver.Login, driver.Password);

                if (token != null)
                {
                    await Task.Run(() => UpdateDriverData(token));
                    return true;
                }
                return false;
            }
            return true;
        }

        private async void UpdateDriverData(string token)
        {
            var _driverDataStore = new DriverDataStore(_client);
            await Task.Run(() => DataService.SaveToken(token));

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
            var accountId = int.Parse(jwtToken.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var driverResponse = await _driverDataStore.GetDriversAsync(accountId);
            var driver = driverResponse.Data.ToList().First();

            if (driver != null)
            {
                await Task.Run(() => DataService.SaveAccount(driver));
            }
        }

        private async Task CheckOldNotification()
        {
            var popupVisible = await SecureStorage.GetAsync("popup_visible");
            if (popupVisible == "true")
            {
                var message = await SecureStorage.GetAsync("popup_message");
                if (!string.IsNullOrEmpty(message))
                {
                    var popup = new CheckControlPopup(message);
                    await PopupNavigation.Instance.PushAsync(popup);
                }
            }
        }


        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }
    }
}
