using CheckDrive.ApiContracts.Car;
using CheckDrive.Mobile.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CheckDrive.Web.Stores.Cars
{
    public interface ICarDataStore
    {
        Task<List<CarDto>> GetCarsAsync();
        Task<CarDto> GetCarAsync(int id);
        Task<CarDto> CreateCarAsync(CarForCreateDto Car);
        Task<CarDto> UpdateCarAsync(int id, CarForUpdateDto Car);
        Task DeleteCarAsync(int id);
    }
}
