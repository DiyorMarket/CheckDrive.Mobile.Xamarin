using CheckDrive.Mobile.DataStores.Dispatcher;
using CheckDrive.Mobile.Services;
using CheckDrive.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var apiClient = new ApiClient();
            var dispatcherReviewDataStore = new DispatcherReviewDataStore(apiClient);
            BindingContext = new HistoryViewModel(dispatcherReviewDataStore);
        }
    }
}