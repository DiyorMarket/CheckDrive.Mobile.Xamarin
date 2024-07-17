using CheckDrive.Mobile.Services;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CheckDrive.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExceptionPage : ContentPage
    {
        public ExceptionPage()
        {
            InitializeComponent();
        }

        private async void OnRetryClicked(object sender, EventArgs e)
        {
            await Task.Run(() => DataService.RemoveAllAcoountData());
            Application.Current.MainPage = new LoginPage();
        }
    }
}