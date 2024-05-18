using CheckDrive.DTOs.Mechanic;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckDrive.Web.Stores.Mechanics
{
    public class MockMechanicDataStore : IMechanicDataStore
    {
        private readonly List<MechanicDto> _mechanics;

        public MockMechanicDataStore()
        {
            _mechanics = new List<MechanicDto>
            {
                new MechanicDto { Id = 1, AccountId = 1 },
                new MechanicDto { Id = 2, AccountId = 2 },
            };
        }

        public async Task<List<MechanicDto>> GetMechanics()
        {
            await Task.Delay(100); 
            return _mechanics.ToList();
        }

        public async Task<MechanicDto> GetMechanic(int id)
        {
            await Task.Delay(100); 
            return _mechanics.FirstOrDefault(m => m.Id == id);
        }

        public async Task<MechanicDto> CreateMechanic(MechanicDto mechanic)
        {
            await Task.Delay(100);
            mechanic.Id = _mechanics.Max(m => m.Id) + 1; 
            _mechanics.Add(mechanic);
            return mechanic;
        }

        public async Task<MechanicDto> UpdateMechanic(int id, MechanicDto mechanic)
        {
            await Task.Delay(100); 
            var existingMechanic = _mechanics.FirstOrDefault(m => m.Id == id);
            if (existingMechanic != null)
            {
                existingMechanic.AccountId = mechanic.AccountId;
            }
            return existingMechanic;
        }

        public async Task DeleteMechanic(int id)
        {
            await Task.Delay(100); 
            var mechanicToRemove = _mechanics.FirstOrDefault(m => m.Id == id);
            if (mechanicToRemove != null)
            {
                _mechanics.Remove(mechanicToRemove);
            }
        }
    }
}
