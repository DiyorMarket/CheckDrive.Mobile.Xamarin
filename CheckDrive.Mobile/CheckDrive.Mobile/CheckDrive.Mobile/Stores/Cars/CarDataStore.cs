using CheckDrive.ApiContracts.Car;
using CheckDrive.Mobile.Responses;
using CheckDrive.Mobile.Services;
using CheckDrive.Web.Stores.Cars;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace CheckDrive.Mobile.Stores.Cars
{
    public class CarDataStore : ICarDataStore
    {
        private readonly ApiClient _api;

        public CarDataStore(ApiClient apiClient)
        {
            _api = apiClient;
        }

        public GetCarResponse GetCarsAsync()
        {
            StringBuilder query = new StringBuilder("");

            var response = _api.GetAsync("cars?" + query.ToString());
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Could not fetch cars.");
            }

            var json = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<GetCarResponse>(json);

            return result;
        }

        public CarDto GetCarAsync(int id)
        {
            var response =  _api.GetAsync($"cars/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Could not fetch cars with id: {id}.");
            }

            var json = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<CarDto>(json);

            return result;
        }

        public CarDto CreateCarAsync(CarForCreateDto car)
        {
            var json = JsonConvert.SerializeObject(car);
            var response = _api.PostAsync("cars", json);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error creating cars.");
            }

            var jsonResponse = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<CarDto>(jsonResponse);
        }
    }
}
