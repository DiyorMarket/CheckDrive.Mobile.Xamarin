using CheckDrive.Domain.DTOs.DispatcherReview;
using CheckDrive.Mobile.DataStores.Dispatcher;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace CheckDrive.Mobile.ViewModels
{
    public class HistoryViewModel 
    {
        private readonly IDispatcherReviewDataStore _dispatcherDataStore;
        
        public ObservableCollection<DispatcherReviewDto> DispatcherReviews { get; private set; }

        public HistoryViewModel(IDispatcherReviewDataStore dispatcherDataStore)
        {
            _dispatcherDataStore = dispatcherDataStore;
            
            DispatcherReviews = new ObservableCollection<DispatcherReviewDto>();

            GetDispatcherReviews();
        }

        public async Task GetDispatcherReviews()
        {
            DispatcherReviews.Clear();

            var items = await _dispatcherDataStore.GetReviewsAsync();

            foreach (var item in items)
            {
                DispatcherReviews.Add(item);
            }
        }
    }
}
