using CheckDrive.ApiContracts.DispatcherReview;
using CheckDrive.Mobile.Responses;
using CheckDrive.Mobile.Services;
using CheckDrive.Web.Stores.DispatcherReviews;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace CheckDrive.Mobile.Stores.DispatcherReviews
{
    public class DispatcheReviewDataStore
    {
        private readonly ApiClient _api;

        public DispatcheReviewDataStore(ApiClient apiClient)
        {
            _api = apiClient;
        }

        public async Task<GetDispatcherReviewResponse> GetDispatcherReviewsAsync()
        {
            StringBuilder query = new StringBuilder("");

            var response = _api.Get("dispatcherreviews?" + query.ToString());
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Could not fetch dispatcherreviews.");
            }

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<GetDispatcherReviewResponse>(json);

            return result;
        }

        public DispatcherReviewDto GetDispatcherReviewAsync(int id)
        {
            var response =  _api.Get($"dispatcherreviews/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Could not fetch dispatcherreview with id: {id}.");
            }

            var json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var result = JsonConvert.DeserializeObject<DispatcherReviewDto>(json);

            return result;
        }

        public async Task<DispatcherReviewForCreateDto> CreateDispatcherReviewAsync(DispatcherReviewForCreateDto dispatcherReview)
        {
            var json = JsonConvert.SerializeObject(dispatcherReview);
            var response = await _api.PostAsync("dispatcherreviews", json);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error creating dispatcherReview.");
            }

            var jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            return JsonConvert.DeserializeObject<DispatcherReviewForCreateDto>(jsonResponse);
        }

        public async Task<DispatcherReviewForUpdateDto> UpdateDispatcherReviewAsync(int id, DispatcherReviewForUpdateDto dispatcherReview)
        {
            var json = JsonConvert.SerializeObject(dispatcherReview);
            var response = await _api.PutAsync($"dispatcherreviews/{dispatcherReview.Id}", json);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error updating dispatcherReview.");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<DispatcherReviewForUpdateDto>(jsonResponse);
        }

        public async Task DeleteDispatcherReviewAsync(int id)
        {
            var response = await _api.DeleteAsync($"dispatcherreviews/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Could not delete dispatcherReview with id: {id}.");
            }
        }

    }
}
