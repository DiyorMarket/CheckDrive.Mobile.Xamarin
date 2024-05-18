using CheckDrive.DTOs.Car;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CheckDrive.Web.Stores.Cars
{
    public interface ICarDataStore
    {
        Task<List<CarDto>> GetCars();
        Task<CarDto> GetCar(int id);
        Task<CarDto> CreateCar(CarDto Car);
        Task<CarDto> UpdateCar(int id, CarDto Car);
        Task DeleteCar(int id);
    }
}
