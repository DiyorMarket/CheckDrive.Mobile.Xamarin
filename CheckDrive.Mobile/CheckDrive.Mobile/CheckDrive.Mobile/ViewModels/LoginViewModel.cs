using CheckDrive.Mobile;
using CheckDrive.Mobile.Services;
using CheckDrive.Mobile.ViewModels;
using CheckDrive.Web.Stores.Accounts;
using CheckDrive.Web.Stores.Drivers;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

public class LoginViewModel : BaseViewModel
{
    private readonly IAccountDataStore _accountDataStore;
    private readonly IDriverDataStore _driverDataStore;

    public ICommand TogglePasswordVisibilityCommand { get; }
    public ICommand ToggleLoginVisibilityCommand { get; }
    public ICommand LoginCommand { get; }

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

    private bool _isLoginError;
    public bool IsLoginError
    {
        get { return _isLoginError; }
        set { SetProperty(ref _isLoginError, value); }
    }

    private bool _isPasswordError;
    public bool IsPasswordError
    {
        get { return _isPasswordError; }
        set { SetProperty(ref _isPasswordError, value); }
    }

    private string _loginErrorMessage;
    public string LoginErrorMessage
    {
        get { return _loginErrorMessage; }
        set { SetProperty(ref _loginErrorMessage, value); }
    }

    private string _passwordErrorMessage;
    public string PasswordErrorMessage
    {
        get { return _passwordErrorMessage; }
        set { SetProperty(ref _passwordErrorMessage, value); }
    }

    public LoginViewModel(IAccountDataStore accountDataStore, IDriverDataStore driverDataStore)
    {
        _accountDataStore = accountDataStore;
        _driverDataStore = driverDataStore;
        TogglePasswordVisibilityCommand = new Command(TogglePasswordVisibility);
        ToggleLoginVisibilityCommand = new Command(ToggleLoginVisibility);
        LoginCommand = new Command(CheckLogin);
    }

    private async void CheckLogin()
    {
        IsBusy = true;

        await PerformLoginTask();

        IsBusy = false;
    }

    private async Task PerformLoginTask()
    {
        var isValid = await ValidateInputs();

        if (!isValid)
        {
            IsBusy = false;
            return;
        }

        try
        {
            bool isSuccess = await CheckingDriverLogin();

            if (isSuccess)
            {
                Application.Current.MainPage = new AppShell();
            }
            else
            {
                LoginErrorMessage = "Login yoki parolni xato kiritdingiz !";
                IsLoginError = true;
                IsPasswordError = true;
            }
            IsBusy = false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Login Error: {ex.Message}");
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Application.Current.MainPage.DisplayAlert("Login Failed", "Please check your credentials and try again.", "OK");
                IsBusy = false;
            });
        }
    }

    private async Task<bool> ValidateInputs()
    {
        LoginErrorMessage = string.Empty;
        PasswordErrorMessage = string.Empty;
        IsLoginError = false;
        IsPasswordError = false;

        bool isValid = true;

        if (string.IsNullOrWhiteSpace(Login))
        {
            LoginErrorMessage = "Login yokin parol xato kiritildi.";
            IsLoginError = true;
            isValid = false;
        }

        if (string.IsNullOrWhiteSpace(Password))
        {
            PasswordErrorMessage = "Password is required.";
            IsPasswordError = true;
            isValid = false;
        }

        return isValid;
    }



    private async Task<bool> CheckingDriverLogin()
    {
        try
        {
            var token = _accountDataStore.CreateTokenAsync(Login, Password).Result;

            if (token != null)
            {
                await Task.Run(() => DataService.SaveToken(token));

                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
                var accountId = int.Parse(jwtToken.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);

                var driverResponse = await _driverDataStore.GetDriversAsync(accountId);
                var driver = driverResponse.Data.ToList().First();

                if (driver != null)
                {
                    driver.Password = Password;
                    await Task.Run(() => DataService.SaveAccount(driver));
                    return true;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Bunday login va password li haydovchi malumotlar omborida mavjud emas !" + ex.Message);
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
