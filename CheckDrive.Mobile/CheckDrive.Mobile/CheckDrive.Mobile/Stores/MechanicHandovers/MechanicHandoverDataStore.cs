using CheckDrive.ApiContracts.MechanicHandover;
using CheckDrive.ApiContracts.OperatorReview;
using CheckDrive.Mobile.Responses;
using CheckDrive.Mobile.Services;
using CheckDrive.Web.Stores.MechanicHandovers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckDrive.Mobile.Stores.MechanicHandovers
{
    public class MechanicHandoverDataStore : IMechanicHandoverDataStore
    {
        private readonly ApiClient _api;

        public MechanicHandoverDataStore(ApiClient apiClient)
        {
            _api = apiClient;
        }
        public GetMechanicHandoverResponse GetMechanicHandovers()
        {
            StringBuilder query = new StringBuilder("");

            var response = _api.Get("mechanics/handovers?" + query.ToString());
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Could not fetch mechanic handovers.");
            }

            var json = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<GetMechanicHandoverResponse>(json);

            return result;
        }
        public MechanicHandoverDto GetMechanicHandover(int id)
        {
            var response = _api.Get($"mechanics/handover/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Could not fetch mechanic handover with id: {id}.");
            }

            var json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var result = JsonConvert.DeserializeObject<MechanicHandoverDto>(json);

            return result;
        }
        public MechanicHandoverDto CreateMechanicHandover(MechanicHandoverForCreateDto mechanicHandover)
        {
            var json = JsonConvert.SerializeObject(mechanicHandover);
            var response = _api.Post("mechanics/handover", json);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error creating mechanic handover.");
            }

            var jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            return JsonConvert.DeserializeObject<MechanicHandoverDto>(jsonResponse);
        }
        public MechanicHandoverDto UpdateMechanicHandover(int id, MechanicHandoverForUpdateDto mechanicHandover)
        {
            var json = JsonConvert.SerializeObject(mechanicHandover);
            var response = _api.Put($"mechanis/handover/{mechanicHandover.Id}", json);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error updating mechanic handover.");
            }

            var jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            return JsonConvert.DeserializeObject<MechanicHandoverDto>(jsonResponse);
        }
        public void DeleteMechanicHandover(int id)
        {
            var response = _api.Delete($"mechanics/handover/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Could not delete mechanic handover with id: {id}.");
            }
        }
    }
}
