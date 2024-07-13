using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CheckDrive.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoInternetPage : ContentPage
    {
        public ICommand RefreshCommand { get; private set; }
        public NoInternetPage()
        {
            InitializeComponent();
            RefreshCommand = new Command(Refresh);
            BindingContext = this;
        }

        private void Refresh()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                // MainPage o'rniga qaytish yoki kerakli sahifaga o'tish
                Application.Current.MainPage = new AppShell();
            }
            else
            {
                DisplayAlert("Eslatma", "Internet hali ham mavjud emas.", "OK");
            }
        }
    }
}
