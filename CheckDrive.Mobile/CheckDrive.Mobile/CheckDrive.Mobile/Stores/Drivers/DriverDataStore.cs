using CheckDrive.ApiContracts.Driver;
using CheckDrive.Mobile.Responses;
using CheckDrive.Mobile.Services;
using CheckDrive.Web.Stores.Drivers;
using Newtonsoft.Json;
using System;
using System.Text;

namespace CheckDrive.Mobile.Stores.Drivers
{
    public class DriverDataStore : IDriverDataStore
    {
        private readonly ApiClient _api;

        public DriverDataStore(ApiClient apiClient)
        {
            _api = apiClient;
        }

        public GetDriverResponse GetDrivers(int accountId)
        {
            StringBuilder query = new StringBuilder("");

            if(!accountId.Equals(0))
            {
                query.Append($"accountId={accountId}");
            }

            var response = _api.Get("drivers?" + query.ToString());

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Could not fetch drivers.");
            }

            var json = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<GetDriverResponse>(json);

            return result;
        }

        public DriverDto GetDriver(int id)
        {
            var response = _api.Get($"drivers?AccountId={id}");

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
