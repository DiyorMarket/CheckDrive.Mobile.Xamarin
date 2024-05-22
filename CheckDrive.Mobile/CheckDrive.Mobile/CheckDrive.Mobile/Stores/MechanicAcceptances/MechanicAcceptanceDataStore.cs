using CheckDrive.ApiContracts.MechanicAcceptance;
using CheckDrive.Mobile.Responses;
using CheckDrive.Mobile.Services;
using CheckDrive.Web.Stores.MechanicAcceptances;
using Newtonsoft.Json;
using System;
using System.Text;

namespace CheckDrive.Mobile.Stores.MechanicAcceptances
{
    public class MechanicAcceptanceDataStore : IMechanicAcceptanceDataStore
    {
        private readonly ApiClient _api;

        public MechanicAcceptanceDataStore(ApiClient apiClient)
        {
            _api = apiClient;
        }

        public GetMechanicAcceptanceResponse GetMechanicAcceptances()
        {
            StringBuilder query = new StringBuilder("");

            var response = _api.Get("accounts?" + query.ToString());
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Could not fetch mechanicAcceptance.");
            }

            var json = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<GetMechanicAcceptanceResponse>(json);

            return result;
        }

        public MechanicAcceptanceDto GetMechanicAcceptance(int id)
        {
            var response = _api.Get($"accounts/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Could not fetch mechanicAcceptance with id: {id}.");
            }

            var json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var result = JsonConvert.DeserializeObject<MechanicAcceptanceDto>(json);

            return result;
        }

        public MechanicAcceptanceDto CreateMechanicAcceptance(MechanicAcceptanceForCreateDto mechanicAcceptance)
        {
            var json = JsonConvert.SerializeObject(mechanicAcceptance);
            var response = _api.Post("accounts", json);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error creating mechanicAcceptance.");
            }

            var jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            return JsonConvert.DeserializeObject<MechanicAcceptanceDto>(jsonResponse);
        }

        public MechanicAcceptanceDto UpdateMechanicAcceptance(int id, MechanicAcceptanceForUpdateDto mechanicAcceptance)
        {
            var json = JsonConvert.SerializeObject(mechanicAcceptance);
            var response = _api.Put($"accounts/{mechanicAcceptance.Id}", json);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error updating mechanicAcceptance.");
            }

            var jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            return JsonConvert.DeserializeObject<MechanicAcceptanceDto>(jsonResponse);
        }

        public void DeleteMechanicAcceptance(int id)
        {
            var response = _api.DeleteAsync($"accounts/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Could not delete mechanicAcceptance with id: {id}.");
            }
        }
    }
}
