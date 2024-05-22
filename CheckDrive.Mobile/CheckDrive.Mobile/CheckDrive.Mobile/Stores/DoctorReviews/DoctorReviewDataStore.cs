using CheckDrive.ApiContracts.DoctorReview;
using CheckDrive.Mobile.Responses;
using CheckDrive.Mobile.Services;
using CheckDrive.Web.Stores.DoctorReviews;
using Newtonsoft.Json;
using System;
using System.Text;

namespace CheckDrive.Mobile.Stores.DoctorReviews
{
    public class DoctorReviewDataStore : IDoctorReviewDataStore
    {
        private readonly ApiClient _api;

        public DoctorReviewDataStore(ApiClient apiClient)
        {
            _api = apiClient;
        }

        public GetDoctorReviewResponse GetDoctorReviews()
        {
            StringBuilder query = new StringBuilder("");

            var response = _api.Get("doctors/reviews?" + query.ToString());
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Could not fetch doctorreviews.");
            }

            var json = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<GetDoctorReviewResponse>(json);

            return result;
        }

        public  DoctorReviewDto GetDoctorReview(int id)
        {
            var response =  _api.Get($"doctors/reviews/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Could not fetch doctorreviews with id: {id}.");
            }

            var json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var result = JsonConvert.DeserializeObject<DoctorReviewDto>(json);

            return result;
        }

        public  DoctorReviewDto CreateDoctorReview(DoctorReviewForCreateDto review)
        {
            var json = JsonConvert.SerializeObject(review);
            var response =  _api.Post("doctors/reviews", json);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error creating doctorreviews.");
            }

            var jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            return JsonConvert.DeserializeObject<DoctorReviewDto>(jsonResponse);
        }

        public DoctorReviewDto UpdateDoctorReview(int id, DoctorReviewForUpdateDto review)
        {
            var json = JsonConvert.SerializeObject(review);
            var response = _api.Put($"doctors/reviews/{review.Id}", json);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error updating doctorreview.");
            }

            var jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            return JsonConvert.DeserializeObject<DoctorReviewDto>(jsonResponse);
        }

        public  void DeleteDoctorReview(int id)
        {
            var response = _api.Delete($"doctors/reviews/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Could not delete doctorreviews with id: {id}.");
            }
        }
    }
}
