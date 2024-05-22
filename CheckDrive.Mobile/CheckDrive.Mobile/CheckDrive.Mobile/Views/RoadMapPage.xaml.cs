using CheckDrive.Mobile.Services;
using CheckDrive.Mobile.Stores.DoctorReviews;
using CheckDrive.Mobile.ViewModels;
using CheckDrive.Web.Stores.DoctorReviews;
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
            BindingContext = roadMapViewModel = new RoadMapViewModel(doctorReviewDS);
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
