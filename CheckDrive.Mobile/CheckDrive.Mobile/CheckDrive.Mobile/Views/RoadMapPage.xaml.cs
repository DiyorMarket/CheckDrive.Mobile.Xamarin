using CheckDrive.Mobile.Services;
using CheckDrive.Mobile.Stores.DispatcherReviewDataStore;
using CheckDrive.Mobile.Stores.DoctorReviews;
using CheckDrive.Mobile.Stores.MechanicAcceptances;
using CheckDrive.Mobile.Stores.MechanicHandovers;
using CheckDrive.Mobile.Stores.OperatorReviews;
using CheckDrive.Mobile.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CheckDrive.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RoadMapPage : ContentPage
    {
        private RoadMapViewModel viewModel;

        public RoadMapPage()
        {
            InitializeComponent();

            var client = new ApiClient();
            var doctorReviewDS = new DoctorReviewDataStore(client);
            var mechanicHandoverDS = new MechanicHandoverDataStore(client);
            var operatorReviewDS = new OperatorReviewDataStore(client);
            var mechanicAcceptanceDS = new MechanicAcceptanceDataStore(client);
            var dispatcherReviewDS = new DispatcherReviewDataStore(client);


            BindingContext = viewModel = new RoadMapViewModel(doctorReviewDS, mechanicAcceptanceDS, operatorReviewDS, mechanicHandoverDS, dispatcherReviewDS);

        }

        private void RoadMapView_Refreshing(Object sender, EventArgs e)
        {
            var client = new ApiClient();
            var doctorReviewDS = new DoctorReviewDataStore(client);
            var mechanicHandoverDS = new MechanicHandoverDataStore(client);
            var operatorReviewDS = new OperatorReviewDataStore(client);
            var mechanicAcceptanceDS = new MechanicAcceptanceDataStore(client);
            var dispatcherReviewDS = new DispatcherReviewDataStore(client);

            BindingContext = viewModel = new RoadMapViewModel(doctorReviewDS, mechanicAcceptanceDS, operatorReviewDS, mechanicHandoverDS, dispatcherReviewDS);

            RoadMapRefresh.IsRefreshing = false;
        }
    }
}
