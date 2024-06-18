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
    public class DoctorReviewDataStore : IDoctorReviewDataStore
    {
        private readonly ApiClient _api;

        public DoctorReviewDataStore(ApiClient apiClient)
        {
            _api = apiClient;
        }

        public GetDoctorReviewResponse GetDoctorReviewsAsync()
        {
            var response = _api.GetAsync("doctors/reviews");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Could not fetch doctor reviews.");
            }

            var json = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<GetDoctorReviewResponse>(json);

            return result;
        }

        public GetDoctorReviewResponse GetDoctorReviewsAsync(DateTime date)
        {
            StringBuilder query = new StringBuilder("");

            if (date != null)
            {
                query = query.Append($"Date={date.Date}&");
            }

            var response =  _api.GetAsync("doctors/reviews?" + query.ToString());
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Could not fetch doctor reviews.");
            }

            var json = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<GetDoctorReviewResponse>(json);

            return result;
        }

        public GetDoctorReviewResponse GetDoctorReviewsByDriverIdAsync(int driverId)
        {
            var response = _api.GetAsync("doctors/reviews?DriverId=" + driverId + "&OrderBy=datedesc");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Could not fetch doctor reviews.");
            }

            var json = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<GetDoctorReviewResponse>(json);

            return result;
        }

        public DoctorReviewDto GetDoctorReviewAsync(int id)
        {
            var response = _api.GetAsync("doctors/reviews?Id=" + id);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Could not fetch doctor reviews.");
            }

            var json = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<DoctorReviewDto>(json);

            return result;
        }

        public DoctorReviewDto CreateDoctorReviewAsync(DoctorReviewForCreateDto review)
        {
            var json = JsonConvert.SerializeObject(review);
            var response =  _api.PostAsync("doctors/reviews", json);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error creating doctor reviews.");
            }

            var jsonResponse = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<DoctorReviewDto>(jsonResponse);
        }
    }
}
