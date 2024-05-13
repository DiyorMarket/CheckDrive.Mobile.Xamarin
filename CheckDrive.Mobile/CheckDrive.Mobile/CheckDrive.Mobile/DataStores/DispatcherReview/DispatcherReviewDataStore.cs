using CheckDrive.Domain.DTOs.DispatcherReview;
using CheckDrive.Mobile.Responses;
using CheckDrive.Mobile.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CheckDrive.Mobile.DataStores.Dispatcher
{
    public class DispatcherReviewDataStore : IDispatcherReviewDataStore
    {
        private readonly ApiClient _api;
        private ApiResponse<DispatcherReviewDto> currentReponse;

        public DispatcherReviewDataStore(ApiClient api)
        {
            _api = api;
        }
        public async Task<IEnumerable<DispatcherReviewDto>> GetReviewsAsync()
        {
            currentReponse = await _api.GetAsync<DispatcherReviewDto>("DispatcherReviews");

            return currentReponse.Data;
        }
    }
}
