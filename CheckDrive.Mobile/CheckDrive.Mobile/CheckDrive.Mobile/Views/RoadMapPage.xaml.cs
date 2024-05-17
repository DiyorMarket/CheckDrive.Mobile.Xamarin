using CheckDrive.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CheckDrive.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RoadMapPage : ContentPage
    {
        private RoadMapViewModel roadMapViewModel;

        public RoadMapPage()
        {
            InitializeComponent();
            BindingContext = roadMapViewModel = new RoadMapViewModel();
        }
    }
}
