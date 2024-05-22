using CheckDrive.ApiContracts.DoctorReview;
using CheckDrive.Mobile.Responses;
using CheckDrive.Mobile.Services;
using CheckDrive.Web.Stores.DoctorReviews;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace CheckDrive.Mobile.Stores.DoctorReviews
{
    public class DoctorReviewDataStore 
    {
        private readonly ApiClient _api;

        public DoctorReviewDataStore(ApiClient apiClient)
        {
            _api = apiClient;
        }

        public async Task<GetDoctorReviewResponse> GetDoctorReviews()
        {
            StringBuilder query = new StringBuilder("");

            var response = await _api.GetAsync("doctors/reviews?" + query.ToString());
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Could not fetch doctorreviews.");
            }

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<GetDoctorReviewResponse>(json);

            return result;
        }

        public async Task<DoctorReviewDto> GetDoctorReview(int id)
        {
            var response = await _api.GetAsync($"doctors/reviews/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Could not fetch doctorreviews with id: {id}.");
            }

            var json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var result = JsonConvert.DeserializeObject<DoctorReviewDto>(json);

            return result;
        }

        public async Task<DoctorReviewForCreateDto> CreateDoctorReview(DoctorReviewForCreateDto review)
        {
            var json = JsonConvert.SerializeObject(review);
            var response = await _api.PostAsync("doctors/reviews", json);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error creating doctorreviews.");
            }

            var jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            return JsonConvert.DeserializeObject<DoctorReviewForCreateDto>(jsonResponse);
        }

        public async Task<DoctorReviewForUpdateDto> UpdateDoctorReview(int id, DoctorReviewForUpdateDto review)
        {
            var json = JsonConvert.SerializeObject(review);
            var response = await _api.PutAsync($"doctors/reviews/{review.Id}", json);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error updating doctorreview.");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<DoctorReviewForUpdateDto>(jsonResponse);
        }

        public async Task DeleteDoctorReview(int id)
        {
            var response = await _api.DeleteAsync($"doctors/reviews/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Could not delete doctorreviews with id: {id}.");
            }
        }

    }
}
