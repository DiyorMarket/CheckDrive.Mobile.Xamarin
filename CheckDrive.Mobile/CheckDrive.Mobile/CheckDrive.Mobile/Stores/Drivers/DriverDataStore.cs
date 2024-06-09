using CheckDrive.Mobile.Responses;
using CheckDrive.Mobile.Services;
using CheckDrive.Web.Stores.Drivers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

        public GetDriverResponse GetDrivers()
        {
            StringBuilder query = new StringBuilder("");

            var response = _api.Get("drivers?" + query.ToString());

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Could not fetch drivers.");
            }

            var json = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<GetDriverResponse>(json);

            return result;
        }
    }
}
