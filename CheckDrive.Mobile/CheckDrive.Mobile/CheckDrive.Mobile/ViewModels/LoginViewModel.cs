using CheckDrive.Mobile.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CheckDrive.Mobile.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
        }

        private async void OnLoginClicked(object obj)
        {
            await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
            IsBusy = true;
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

                Application.Current.MainPage = new AppShell();
            }
            catch
            {
                Application.Current.MainPage.DisplayAlert("Login Failed", "Please check your credentials and try again.", "OK");
            }
            IsBusy = false;
        }

        private bool CheckingDriverLogin()
        {
            IsBusy = true;

            var token = _accountDataStore.CreateToken(Login, Password);

            if (token != null)
            {
                DataService.SaveToken(token);
            }

            var drivers = _accountDataStore.GetAccounts(Login).Data.ToList();
            var driver = drivers[0];

            if (driver != null)
            {
                DataService.SaveAccount(driver);
                return true;
            }

            IsBusy= false;

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
