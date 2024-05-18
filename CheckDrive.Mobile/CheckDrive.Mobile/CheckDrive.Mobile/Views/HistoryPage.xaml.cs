using CheckDrive.Mobile.ViewModels;
using CheckDrive.Web.Stores.DispatcherReviews;
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

            var dispatcherReviewDataStore = new MockDispatcherReviewDataStore();
            var doctorReviewDataStore = new MockDoctorReviewDataStore();

            BindingContext = new HistoryViewModel(dispatcherReviewDataStore, doctorReviewDataStore);
        }
    }
}