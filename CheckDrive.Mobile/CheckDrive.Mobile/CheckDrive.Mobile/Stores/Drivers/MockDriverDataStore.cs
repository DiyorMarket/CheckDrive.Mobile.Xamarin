using CheckDrive.DTOs.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckDrive.Web.Stores.Drivers
{
    public class MockDriverDataStore : IDriverDataStore
    {
        private readonly List<DriverDto> _drivers;

        public MockDriverDataStore()
        {
            _drivers = new List<DriverDto>
            {
                new DriverDto { Id = 1, AccountId = 1 },
                new DriverDto { Id = 2, AccountId = 2 },
            };
        }

        public async Task<List<DriverDto>> GetDrivers()
        {
            await Task.Delay(100); 
            return _drivers.ToList();
        }

        public async Task<DriverDto> GetDriver(int id)
        {
            await Task.Delay(100); 
            return _drivers.FirstOrDefault(d => d.Id == id);
        }

        public async Task<DriverDto> CreateDriver(DriverDto driver)
        {
            await Task.Delay(100); 
            driver.Id = _drivers.Max(d => d.Id) + 1; 
            _drivers.Add(driver);
            return driver;
        }

        public async Task<DriverDto> UpdateDriver(int id, DriverDto driver)
        {
            await Task.Delay(100); 
            var existingDriver = _drivers.FirstOrDefault(d => d.Id == id);
            if (existingDriver != null)
            {
                existingDriver.AccountId = driver.AccountId;
               
            }
            return existingDriver;
        }

        public async Task DeleteDriver(int id)
        {
            await Task.Delay(100); 
            var driverToRemove = _drivers.FirstOrDefault(d => d.Id == id);
            if (driverToRemove != null)
            {
                _drivers.Remove(driverToRemove);
            }
        }
    }
}
