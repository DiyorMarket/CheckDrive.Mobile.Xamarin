using CheckDrive.ApiContracts.DoctorReview;
using CheckDrive.ApiContracts.Operator;
using CheckDrive.ApiContracts.OperatorReview;
using CheckDrive.Mobile.Responses;
using CheckDrive.Mobile.Services;
using CheckDrive.Web.Stores.OperatorReviews;
using CheckDrive.Web.Stores.Operators;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        public GetOperatorReviewResponse GetOperatorReviews()
        {
            StringBuilder query = new StringBuilder("");

            var response = _api.Get("operators/reviews?" + query.ToString());
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Could not fetch operator reviews.");
            }

            var json = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<GetOperatorReviewResponse>(json);

            return result;
        }
        public GetOperatorReviewResponse GetOperatorReviewsByDriverId(int driverId)
        {
            var response = _api.Get("mechanics/handovers?DriverId=" + driverId);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Could not fetch operator reviews.");
            }

            var json = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<GetOperatorReviewResponse>(json);

            return result;
        }
        public OperatorReviewDto GetOperatorReview(int id)
        {
            var response = _api.Get($"operators/review/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Could not fetch operator reviews with id: {id}.");
            }

            var json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var result = JsonConvert.DeserializeObject<OperatorReviewDto>(json);

            return result;
        }

        public OperatorReviewDto CreateOperatorReview(OperatorReviewForCreateDto operatorReview)
        {
            var json = JsonConvert.SerializeObject(operatorReview);
            var response = _api.Post("operators/review", json);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error creating operator reviews.");
            }

            var jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            return JsonConvert.DeserializeObject<OperatorReviewDto>(jsonResponse);
        }
        public OperatorReviewDto UpdateOperatorReview(int id, OperatorReviewForUpdateDto operatorReview)
        {
            var json = JsonConvert.SerializeObject(operatorReview);
            var response = _api.Put($"doctors/review/{operatorReview.Id}", json);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error updating operator review.");
            }

            var jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            return JsonConvert.DeserializeObject<OperatorReviewDto>(jsonResponse);
        }

        public void DeleteOperatorReview(int id)
        {
            var response = _api.Delete($"operators/review/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Could not delete operator review with id: {id}.");
            }
        }
    }
}
