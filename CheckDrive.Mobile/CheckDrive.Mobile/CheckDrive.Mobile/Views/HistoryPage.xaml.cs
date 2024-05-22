using CheckDrive.Mobile.Services;
using CheckDrive.Mobile.Stores.DispatcherReviews;
using CheckDrive.Mobile.Stores.DoctorReviews;
using CheckDrive.Mobile.ViewModels;
using CheckDrive.Web.Stores.DispatcherReviews;
using CheckDrive.Web.Stores.Dispatchers;
using CheckDrive.Web.Stores.DoctorReviews;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CheckDrive.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryPage : ContentPage
    {
        public HistoryPage()
        {
            InitializeComponent();

            var client = new ApiClient();
            //var dispatcherreviewDS = new DispatcheReviewDataStore(client);
            var doctorReviewDS = new DoctorReviewDataStore(client);

            var dispatcherreviewDS = new MockDispatcherReviewDataStore();

            BindingContext = new HistoryViewModel(dispatcherreviewDS, doctorReviewDS);
        }
    }
}