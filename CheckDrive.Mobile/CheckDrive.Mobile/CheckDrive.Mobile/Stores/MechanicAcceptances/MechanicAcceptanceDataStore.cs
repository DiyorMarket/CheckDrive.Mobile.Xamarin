using CheckDrive.ApiContracts.MechanicAcceptance;
using CheckDrive.Mobile.Responses;
using CheckDrive.Mobile.Services;
using CheckDrive.Web.Stores.MechanicAcceptances;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace CheckDrive.Mobile.Stores.MechanicAcceptances
{
    public class MechanicAcceptanceDataStore : IMechanicAcceptanceDataStore
    {
        private readonly ApiClient _api;

        public MechanicAcceptanceDataStore(ApiClient apiClient)
        {
            _api = apiClient;
        }

        public GetMechanicAcceptanceResponse GetMechanicAcceptancesAsync(int driverId, string sortString)
        {
            StringBuilder query = new StringBuilder("");

            if (!driverId.Equals(0))
            {
                query = query.Append($"DriverId={driverId}&");
            }

            if (!string.IsNullOrEmpty(sortString))
            {
                query = query.Append($"Sort={sortString}&");
            }

            var response = _api.GetAsync("mechanics/acceptances?" + query.ToString());
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Could not fetch mechanic acceptance.");
            }

            var json =  response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<GetMechanicAcceptanceResponse>(json);

            return result;
        }

        public GetMechanicAcceptanceResponse GetMechanicAcceptancesByDriverIdAsync(int driverId)
        {
            var response = _api.GetAsync("mechanics/acceptances?DriverId=" + driverId + "&OrderBy=datedesc");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Could not fetch mechanic acceptance.");
            }

            var json = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<GetMechanicAcceptanceResponse>(json);

            return result;
        }

        public GetMechanicAcceptanceResponse GetMechanicAcceptancesAsync(DateTime date)
        {
            StringBuilder query = new StringBuilder("");

            if (date > DateTime.MinValue)
            {
                query = query.Append($"Date={date.Date}");
            }

            var response = _api.GetAsync("mechanics/acceptances?" + query.ToString());
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Could not fetch mechanic acceptance.");
            }

            var json = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<GetMechanicAcceptanceResponse>(json);

            return result;
        }

        public GetMechanicAcceptanceResponse GetMechanicAcceptancesAsync()
        {
            var response = _api.GetAsync("mechanics/acceptances");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Could not fetch mechanic acceptance.");
            }

            var json = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<GetMechanicAcceptanceResponse>(json);

            return result;
        }

        public MechanicAcceptanceDto GetMechanicAcceptanceAsync(int id)
        {
            var response = _api.GetAsync($"mechanics/acceptances/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Could not fetch mechanic acceptance with id: {id}.");
            }

            var json = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<MechanicAcceptanceDto>(json);

            return result;
        }

        public MechanicAcceptanceDto CreateMechanicAcceptanceAsync(MechanicAcceptanceForCreateDto mechanicAcceptance)
        {
            var json = JsonConvert.SerializeObject(mechanicAcceptance);
            var response = _api.PostAsync("mechanics/acceptances", json);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error creating mechanic acceptance.");
            }

            var jsonResponse = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<MechanicAcceptanceDto>(jsonResponse);
        }
    }
}
