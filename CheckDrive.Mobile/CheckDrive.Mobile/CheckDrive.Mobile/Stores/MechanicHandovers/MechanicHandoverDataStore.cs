using CheckDrive.ApiContracts.MechanicHandover;
using CheckDrive.Mobile.Responses;
using CheckDrive.Mobile.Services;
using CheckDrive.Web.Stores.MechanicHandovers;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace CheckDrive.Mobile.Stores.MechanicHandovers
{
    public class MechanicHandoverDataStore : IMechanicHandoverDataStore
    {
        private readonly ApiClient _api;

        public MechanicHandoverDataStore(ApiClient apiClient)
        {
            _api = apiClient;
        }

        public GetMechanicHandoverResponse GetMechanicHandoversAsync()
        {
            var response = _api.GetAsync("mechanics/handovers");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Could not fetch mechanic handovers.");
            }

            var json = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<GetMechanicHandoverResponse>(json);

            return result;
        }

        public GetMechanicHandoverResponse GetMechanicHandoversAsync(DateTime date)
        {
            StringBuilder query = new StringBuilder("");

            if (date != DateTime.MinValue)
            {
                query.Append($"Date={date.Date}&");
            }

            var response = _api.GetAsync("mechanics/handovers?" + query.ToString());
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Could not fetch mechanic handovers.");
            }

            var json = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<GetMechanicHandoverResponse>(json);

            return result;
        }

        public GetMechanicHandoverResponse GetMechanicHandoversByDriverIdAsync(int driverId)
        {
            var response = _api.GetAsync("mechanics/handovers?DriverId=" + driverId + "&OrderBy=datedesc");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Could not fetch mechanic handovers.");
            }

            var json = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<GetMechanicHandoverResponse>(json);

            return result;
        }

        public MechanicHandoverDto GetMechanicHandoverAsync(int id)
        {
            var response = _api.GetAsync($"mechanics/handover/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Could not fetch mechanic handover with id: {id}.");
            }

            var json = 
            response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<MechanicHandoverDto>(json);

            return result;
        }

        public MechanicHandoverDto CreateMechanicHandoverAsync(MechanicHandoverForCreateDto mechanicHandover)
        {
            var json = JsonConvert.SerializeObject(mechanicHandover);
            var response = _api.PostAsync("mechanics/handover", json);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error creating mechanic handover.");
            }

            var jsonResponse = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<MechanicHandoverDto>(jsonResponse);
        }
    }
}
