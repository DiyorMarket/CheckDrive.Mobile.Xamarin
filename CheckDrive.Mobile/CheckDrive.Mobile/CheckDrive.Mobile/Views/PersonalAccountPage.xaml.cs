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

            BindingContext = new PersonalAccountViewModel();
        }
    }
}