using CheckDrive.ApiContracts.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CheckDrive.Web.Stores.Drivers
{
    public interface IDriverDataStore
    {
        List<DriverDto> GetDrivers();
        DriverDto GetDriver(int id);
        DriverDto CreateDriver(DriverDto driver);
        void DeleteDriver(int id);
    }
}
