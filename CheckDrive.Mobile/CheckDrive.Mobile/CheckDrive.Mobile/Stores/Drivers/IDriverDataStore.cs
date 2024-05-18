using CheckDrive.DTOs.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CheckDrive.Web.Stores.Drivers
{
    public interface IDriverDataStore
    {
        Task<List<DriverDto>> GetDrivers();
        Task<DriverDto> GetDriver(int id);
        Task<DriverDto> CreateDriver(DriverDto driver);
        Task<DriverDto> UpdateDriver(int id, DriverDto driver);
        Task DeleteDriver(int id);
    }
}
