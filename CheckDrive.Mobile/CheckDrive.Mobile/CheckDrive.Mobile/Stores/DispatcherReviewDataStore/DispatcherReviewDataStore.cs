using CheckDrive.Mobile.Responses;
using CheckDrive.Mobile.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CheckDrive.Mobile.Stores.DispatcherReviewDataStore
{
    public class DispatcherReviewDataStore : IDispatcherReviewDataStore
    {
        private readonly ApiClient _api;

        public DispatcherReviewDataStore(ApiClient api)
        {
            _api = api;
        }

        public async Task<GetDispatcherReviewResponse> GetDispatcherReviewResponses(int driverId)
        {
            StringBuilder query = new StringBuilder("");

            if (driverId != 0)
            {
                query = query.Append($"DriverId={driverId}");
            }

            var response = await _api.GetAsync("dispatchers/reviews?" + query.ToString());
            if (!response.IsSuccessStatusCode)
            {
                Application.Current.MainPage = new AppShell();
                throw new Exception("Could not fetch dispatcher reviews.");
            }

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<GetDispatcherReviewResponse>(json);

            return result;
        }
    }
}
