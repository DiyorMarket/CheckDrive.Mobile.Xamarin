using CheckDrive.Mobile.Services;
using CheckDrive.Mobile.Stores.DoctorReviews;
using CheckDrive.Mobile.Stores.MechanicAcceptances;
using CheckDrive.Mobile.Stores.MechanicHandovers;
using CheckDrive.Mobile.Stores.OperatorReviews;
using CheckDrive.Mobile.ViewModels;
using CheckDrive.Web.Stores.DoctorReviews;
using CheckDrive.Web.Stores.MechanicHandovers;
using CheckDrive.Web.Stores.OperatorReviews;
using System;
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

            var client = new ApiClient();
            var doctorReviewDS = new DoctorReviewDataStore(client);
            var mechanicHandoverDS = new MechanicHandoverDataStore(client);
            var operatorReviewDS = new OperatorReviewDataStore(client);
            var mechanicAcceptanceDS = new MechanicAcceptanceDataStore(client);

            BindingContext = roadMapViewModel = new RoadMapViewModel(doctorReviewDS, mechanicAcceptanceDS, operatorReviewDS, mechanicHandoverDS);
        }
        private async void ClickToShowPopup_Clicked(object sender, EventArgs e)
        {
            var popup = new CheckControlPopup()
            {
                BindingContext = BindingContext
            };

            await Navigation.PushModalAsync(popup);
        }
    }
}
