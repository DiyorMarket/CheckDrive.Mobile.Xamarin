using CheckDrive.ApiContracts.Car;
using CheckDrive.Mobile.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CheckDrive.Web.Stores.Cars
{
    public interface ICarDataStore
    {
        GetCarResponse GetCarsAsync();
        CarDto GetCarAsync(int id);
        CarDto CreateCarAsync(CarForCreateDto Car);
    }
}
