using CheckDrive.Mobile.Services;
using CheckDrive.Mobile.Stores.Drivers;
using CheckDrive.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CheckDrive.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PersonalAccountPage : ContentPage
    {

        public PersonalAccountPage()
        {
            InitializeComponent();

            var client = new ApiClient();
            var store = new DriverDataStore(client);
            var viewModel = new PersonalAccountViewModel(store);
            BindingContext = viewModel;
        }
    }
}