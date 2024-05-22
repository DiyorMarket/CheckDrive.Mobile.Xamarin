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
	public partial class CheckControlPopup : ContentPage
	{
        public CheckControlPopup ()
        {
			InitializeComponent ();

            var client = new ApiClient();
            var doctorReviewDS = new DoctorReviewDataStore(client);

            var viewModel = new RoadMapViewModel(doctorReviewDS);
            BindingContext = viewModel;
        }
        private async void CancelButton_Clicked(object sender, EventArgs e)
        {
            await Navigation?.PopModalAsync();
        }
        private async void OkButton_Clicked(object sender, EventArgs e)
        {
            await Navigation?.PopModalAsync();
        }
    }
}