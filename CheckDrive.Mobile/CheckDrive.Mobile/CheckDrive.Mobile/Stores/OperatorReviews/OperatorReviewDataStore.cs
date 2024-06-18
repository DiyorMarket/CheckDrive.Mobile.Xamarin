using CheckDrive.ApiContracts.OperatorReview;
using CheckDrive.Mobile.Responses;
using CheckDrive.Mobile.Services;
using CheckDrive.Web.Stores.OperatorReviews;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace CheckDrive.Mobile.Stores.OperatorReviews
{
    public class OperatorReviewDataStore : IOperatorReviewDataStore
    {
        private readonly ApiClient _api;

        public OperatorReviewDataStore(ApiClient apiClient)
        {
            _api = apiClient;
        }

        public GetOperatorReviewResponse GetOperatorReviewsAsync()
        {
            var response = _api.GetAsync("operators/reviews");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Could not fetch operator reviews.");
            }

            var json =  response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<GetOperatorReviewResponse>(json);

            return result;
        }

        public GetOperatorReviewResponse GetOperatorReviewsAsync(DateTime date)
        {
            StringBuilder query = new StringBuilder("");

            if (date != DateTime.MinValue)
            {
                query.Append($"Date={date.Date}&");
            }

            var response = _api.GetAsync("operators/reviews?" + query.ToString());
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Could not fetch operator reviews.");
            }

            var json = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<GetOperatorReviewResponse>(json);

            return result;
        }

        public GetOperatorReviewResponse GetOperatorReviewsByDriverIdAsync(int driverId)
        {
            StringBuilder query = new StringBuilder("");

            if (!driverId.Equals(0))
            {
                query.Append($"driverId={driverId}");
            }

            var response = _api.GetAsync("operators/reviews?" + query.ToString());
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Could not fetch operator reviews.");
            }

            var json = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<GetOperatorReviewResponse>(json);

            return result;
        }

        public OperatorReviewDto GetOperatorReviewAsync(int id)
        {
            var response = _api.GetAsync($"operators/review/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Could not fetch operator reviews with id: {id}.");
            }

            var json = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<OperatorReviewDto>(json);

            return result;
        }

        public OperatorReviewDto CreateOperatorReviewAsync(OperatorReviewForCreateDto operatorReview)
        {
            var json = JsonConvert.SerializeObject(operatorReview);
            var response = _api.PostAsync("operators/review", json);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error creating operator reviews.");
            }

            var jsonResponse = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<OperatorReviewDto>(jsonResponse);
        }
    }
}
