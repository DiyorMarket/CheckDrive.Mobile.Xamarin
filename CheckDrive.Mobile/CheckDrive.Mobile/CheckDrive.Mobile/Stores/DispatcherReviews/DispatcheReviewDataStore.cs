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
    public class DispatcheReviewDataStore : IDispatcherReviewDataStore
    {
        private readonly ApiClient _api;

        public DispatcheReviewDataStore(ApiClient apiClient)
        {
            _api = apiClient;
        }

        public GetDispatcherReviewResponse GetDispatcherReviewsAsync()
        {
            StringBuilder query = new StringBuilder("");

            var response = _api.GetAsync("dispatchers/reviews?" + query.ToString());
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Could not fetch dispatcher reviews.");
            }

            var json = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<GetDispatcherReviewResponse>(json);

            return result;
        }

        public DispatcherReviewDto GetDispatcherReviewAsync(int id)
        {
            var response = _api.GetAsync($"dispatchers/reviews/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Could not fetch dispatcher review with id: {id}.");
            }

            var json = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<DispatcherReviewDto>(json);

            return result;
        }

        public DispatcherReviewDto CreateDispatcherReviewAsync(DispatcherReviewForCreateDto dispatcherReview)
        {
            var json = JsonConvert.SerializeObject(dispatcherReview);
            var response =  _api.PostAsync("dispatchers/reviews", json);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error creating dispatcher review.");
            }

            var jsonResponse =  response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<DispatcherReviewDto>(jsonResponse);
        }
    }
}
