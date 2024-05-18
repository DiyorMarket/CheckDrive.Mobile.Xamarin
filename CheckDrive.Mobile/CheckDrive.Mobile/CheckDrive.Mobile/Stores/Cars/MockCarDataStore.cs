using CheckDrive.DTOs.Car;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckDrive.Web.Stores.Cars
{
    public class MockCarDataStore : ICarDataStore
    {
        private readonly List<CarDto> _Cars;

        public MockCarDataStore()
        {
            _Cars = new List<CarDto>
            {
                new CarDto { Id = 1, Model = "Toyota Camry", Color = "Red", Number = "ABC123", MeduimFuelConsumption = 8.5, FuelTankCapacity = 60, ManufacturedYear = 2020 },
                new CarDto { Id = 2, Model = "Honda Civic", Color = "Blue", Number = "DEF456", MeduimFuelConsumption = 7.2, FuelTankCapacity = 55, ManufacturedYear = 2019 },
            };
        }

        public async Task<List<CarDto>> GetCars()
        {
            await Task.Delay(100); 
            return _Cars.ToList();
        }

        public async Task<CarDto> GetCar(int id)
        {
            await Task.Delay(100);
            return _Cars.FirstOrDefault(c => c.Id == id);
        }

        public async Task<CarDto> CreateCar(CarDto Car)
        {
            await Task.Delay(100); 
            Car.Id = _Cars.Max(c => c.Id) + 1;
            _Cars.Add(Car);
            return Car;
        }

        public async Task<CarDto> UpdateCar(int id, CarDto Car)
        {
            await Task.Delay(100);
            var existingCar = _Cars.FirstOrDefault(c => c.Id == id);
            if (existingCar != null)
            {
                existingCar.Model = Car.Model;
                existingCar.Color = Car.Color;
                existingCar.Number = Car.Number;
                existingCar.MeduimFuelConsumption = Car.MeduimFuelConsumption;
                existingCar.FuelTankCapacity = Car.FuelTankCapacity;
                existingCar.ManufacturedYear = Car.ManufacturedYear;
            }
            return existingCar;
        }

        public async Task DeleteCar(int id)
        {
            await Task.Delay(100);
            var CarToRemove = _Cars.FirstOrDefault(c => c.Id == id);
            if (CarToRemove != null)
            {
                _Cars.Remove(CarToRemove);
            }
        }
    }
}
