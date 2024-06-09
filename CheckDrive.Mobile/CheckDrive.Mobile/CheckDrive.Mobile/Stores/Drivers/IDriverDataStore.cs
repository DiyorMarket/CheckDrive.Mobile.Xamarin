using CheckDrive.ApiContracts.Driver;
using CheckDrive.Mobile.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CheckDrive.Web.Stores.Drivers
{
    public interface IDriverDataStore
    {
        GetDriverResponse GetDrivers();
    }
}
