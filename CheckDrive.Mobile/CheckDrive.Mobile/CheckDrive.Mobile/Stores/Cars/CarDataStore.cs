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
    public class CarDataStore
    {
        private readonly ApiClient _api;

        public CarDataStore(ApiClient apiClient)
        {
            _api = apiClient;
        }

        public async Task<GetCarResponse> GetCarsAsync()
        {
            StringBuilder query = new StringBuilder("");

            var response = await _api.GetAsync("cars?" + query.ToString());
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Could not fetch cars.");
            }

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<GetCarResponse>(json);

            return result;
        }

        public async Task<CarDto> GetCarAsync(int id)
        {
            var response = await _api.GetAsync($"cars/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Could not fetch cars with id: {id}.");
            }

            var json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var result = JsonConvert.DeserializeObject<CarDto>(json);

            return result;
        }

        public async Task<CarForCreateDto> CreateCarAsync(CarForCreateDto Car)
        {
            var json = JsonConvert.SerializeObject(Car);
            var response = await _api.PostAsync("cars", json);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error creating accounts.");
            }

            var jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            return JsonConvert.DeserializeObject<CarForCreateDto>(jsonResponse);
        }

        public async Task<CarForUpdateDto> UpdateCarAsync(int id, CarForUpdateDto car)
        {
            var json = JsonConvert.SerializeObject(car);
            var response = await _api.PutAsync($"cars/{car.Id}", json);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error updating car.");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<CarForUpdateDto>(jsonResponse);
        }

        public async Task DeleteCarAsync(int id)
        {
            var response = await _api.DeleteAsync($"cars/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Could not delete accounts with id: {id}.");
            }
        }
    }
}
