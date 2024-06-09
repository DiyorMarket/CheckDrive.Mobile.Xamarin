using CheckDrive.ApiContracts.Car;
using CheckDrive.Mobile.Responses;
using System.Collections.Generic;

namespace CheckDrive.Web.Stores.Cars
{
    public interface ICarDataStore
    {
        GetCarResponse GetCars();
        CarDto GetCar(int id);
        CarDto CreateCar(CarForCreateDto Car);
        CarDto UpdateCar(int id, CarForUpdateDto Car);
        void DeleteCar(int id);
    }
}
