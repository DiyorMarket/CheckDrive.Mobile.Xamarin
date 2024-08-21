using CheckDrive.Mobile.Services;
using CheckDrive.Mobile.Stores.DispatcherReviewDataStore;
using CheckDrive.Mobile.Stores.DoctorReviews;
using CheckDrive.Mobile.Stores.MechanicAcceptances;
using CheckDrive.Mobile.Stores.MechanicHandovers;
using CheckDrive.Mobile.Stores.OperatorReviews;
using CheckDrive.Mobile.ViewModels;
using Rg.Plugins.Popup.Pages;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CheckDrive.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CheckControlPopup : PopupPage
    {
        private RoadMapViewModel _mapViewModel;

        public CheckControlPopup (string mes)
        {
			InitializeComponent ();

            var client = new ApiClient();
            var doctorReviewDS = new DoctorReviewDataStore(client);
            var mechanicHandoverDS = new MechanicHandoverDataStore(client);
            var operatorReviewDS = new OperatorReviewDataStore(client);
            var mechanicAcceptanceDS = new MechanicAcceptanceDataStore(client);
            var dispatcherReviewDS = new DispatcherReviewDataStore(client);

            _mapViewModel = new RoadMapViewModel(doctorReviewDS, mechanicAcceptanceDS, operatorReviewDS, mechanicHandoverDS, dispatcherReviewDS);
            _mapViewModel.Message = mes;

            BindingContext = _mapViewModel;
        }

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();
        //    _mapViewModel.LoadViewPage();
        //}

        private async void PopupPage_BackgroundClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}