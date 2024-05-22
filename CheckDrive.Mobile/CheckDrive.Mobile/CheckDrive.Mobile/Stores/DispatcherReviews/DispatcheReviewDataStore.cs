using CheckDrive.ApiContracts.DispatcherReview;
using CheckDrive.Mobile.Responses;
using CheckDrive.Mobile.Services;
using CheckDrive.Web.Stores.DispatcherReviews;
using Newtonsoft.Json;
using System;
using System.Text;

namespace CheckDrive.Mobile.Stores.DispatcherReviews
{
    public class DispatcheReviewDataStore : IDispatcherReviewDataStore
    {
        private readonly ApiClient _api;

        public DispatcheReviewDataStore(ApiClient apiClient)
        {
            _api = apiClient;
        }

        public GetDispatcherReviewResponse GetDispatcherReviews()
        {
            StringBuilder query = new StringBuilder("");

            var response = _api.Get("dispatcherreviews?" + query.ToString());
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Could not fetch dispatcherreviews.");
            }

            var json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var result = JsonConvert.DeserializeObject<GetDispatcherReviewResponse>(json);

            return result;
        }

        public DispatcherReviewDto GetDispatcherReview(int id)
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

        public DispatcherReviewDto CreateDispatcherReview(DispatcherReviewForCreateDto dispatcherReview)
        {
            var json = JsonConvert.SerializeObject(dispatcherReview);
            var response = _api.Post("dispatcherreviews", json);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error creating dispatcherReview.");
            }

            var jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            return JsonConvert.DeserializeObject<DispatcherReviewDto>(jsonResponse);
        }

        public DispatcherReviewDto UpdateDispatcherReview(int id, DispatcherReviewForUpdateDto dispatcherReview)
        {
            var json = JsonConvert.SerializeObject(dispatcherReview);
            var response = _api.Put($"dispatcherreviews/{dispatcherReview.Id}", json);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error updating dispatcherReview.");
            }

            var jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            return JsonConvert.DeserializeObject<DispatcherReviewDto>(jsonResponse);
        }

        public void DeleteDispatcherReview(int id)
        {
            var response = _api.DeleteAsync($"dispatcherreviews/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Could not delete dispatcherReview with id: {id}.");
            }
        }

    }
}
