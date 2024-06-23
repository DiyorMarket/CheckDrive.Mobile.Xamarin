using CheckDrive.ApiContracts.Driver;
using CheckDrive.Mobile.Services;
using CheckDrive.Mobile.Stores.Accounts;
using CheckDrive.Mobile.Views;
using System;
using Xamarin.Forms;

namespace CheckDrive.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var isChecked = CheckloginDate();

            if (isChecked)
            {
                MainPage = new AppShell();
                return;
            }
            MainPage = new LoginPage();
        }

        private bool CheckloginDate()
        {
            var creationDate = DataService.GetCreationDate();
            var driver = DataService.GetAccount();

            if (creationDate != null && driver != null && DateTime.Now.Date.AddDays(-30) <= creationDate.Date)
            {
                CheckTokenDate(driver);
                return true;
            }

            DataService.RemoveAllAcoountData();

            return false;
        }

        private async void CheckTokenDate(DriverDto driver)
        {
            var creationTokenDate = DataService.GetTokenCreationDate();
            var summHours = DateTime.Now - creationTokenDate;

            if (summHours.TotalHours >= 12)
            {
                var accaountDS = new AccountDataStore(new ApiClient());

                var token = await accaountDS.CreateTokenAsync(driver.Login, driver.Password);

                if (token != null)
                {
                    DataService.SaveToken(token);
                }
            }
        }


        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
