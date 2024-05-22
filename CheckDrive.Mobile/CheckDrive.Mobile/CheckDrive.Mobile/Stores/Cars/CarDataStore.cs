using CheckDrive.ApiContracts.Car;
using CheckDrive.Mobile.Responses;
using CheckDrive.Mobile.Services;
using CheckDrive.Web.Stores.Cars;
using Newtonsoft.Json;
using System;
using System.Text;

namespace CheckDrive.Mobile.Stores.Cars
{
    public class CarDataStore : ICarDataStore
    {
        private readonly ApiClient _api;

        public CarDataStore(ApiClient apiClient)
        {
            _api = apiClient;
        }

        public GetCarResponse GetCars()
        {
            StringBuilder query = new StringBuilder("");

            var response = _api.Get("cars?" + query.ToString());
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Could not fetch cars.");
            }

            var json = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<GetCarResponse>(json);

            return result;
        }

        public CarDto GetCar(int id)
        {
            var response = _api.Get($"cars/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Could not fetch cars with id: {id}.");
            }

            var json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var result = JsonConvert.DeserializeObject<CarDto>(json);

            return result;
        }

        public CarDto CreateCar(CarForCreateDto Car)
        {
            var json = JsonConvert.SerializeObject(Car);
            var response = _api.Post("cars", json);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error creating cars.");
            }

            var jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            return JsonConvert.DeserializeObject<CarDto>(jsonResponse);
        }

        public CarDto UpdateCar(int id, CarForUpdateDto car)
        {
            var json = JsonConvert.SerializeObject(car);
            var response = _api.Put($"cars/{car.Id}", json);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error updating car.");
            }

            var jsonResponse = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<CarDto>(jsonResponse);
        }

        public void DeleteCar(int id)
        {
            var response = _api.Delete($"cars/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Could not delete cars with id: {id}.");
            }
        }
    }
}
