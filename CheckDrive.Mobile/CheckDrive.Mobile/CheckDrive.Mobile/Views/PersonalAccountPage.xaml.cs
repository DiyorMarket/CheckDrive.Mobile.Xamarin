using CheckDrive.Mobile.ViewModels;
using CheckDrive.Web.Stores.Drivers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CheckDrive.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PersonalAccountPage : ContentPage
    {
        private PersonalAccountViewModel viewModel;
        private readonly IDriverDataStore store;

        public PersonalAccountPage()
        {
            InitializeComponent();
            store = new MockDriverDataStore();
            BindingContext = viewModel = new PersonalAccountViewModel(store);
        }
    }
}