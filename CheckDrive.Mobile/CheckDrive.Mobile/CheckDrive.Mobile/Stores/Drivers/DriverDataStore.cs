using CheckDrive.ApiContracts.Driver;
using CheckDrive.Mobile.Responses;
using CheckDrive.Mobile.Services;
using CheckDrive.Web.Stores.Drivers;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace CheckDrive.Mobile.Stores.Drivers
{
    public class DriverDataStore : IDriverDataStore
    {
        private readonly ApiClient _api;

        public DriverDataStore(ApiClient apiClient)
        {
            _api = apiClient;
        }

        public GetDriverResponse GetDriversAsync(int accountId)
        {
            StringBuilder query = new StringBuilder("");

            if (!accountId.Equals(0))
            {
                query.Append($"accountId={accountId}");
            }

            var response = _api.GetAsync("drivers?" + query.ToString());

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Could not fetch drivers.");
            }

            var json = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<GetDriverResponse>(json);

            return result;
        }

        public DriverDto GetDriverAsync(int id)
        {
            var response = _api.GetAsync($"drivers?AccountId={id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Could not fetch drivers.");
            }

            var json = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<DriverDto>(json);

            return result;
        }
    }
}
