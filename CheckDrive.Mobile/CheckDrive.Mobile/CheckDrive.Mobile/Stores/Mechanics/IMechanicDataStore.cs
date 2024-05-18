using CheckDrive.DTOs.Mechanic;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CheckDrive.Web.Stores.Mechanics
{
    public interface IMechanicDataStore
    {
        Task<List<MechanicDto>> GetMechanics();
        Task<MechanicDto> GetMechanic(int id);
        Task<MechanicDto> CreateMechanic(MechanicDto mechanic);
        Task<MechanicDto> UpdateMechanic(int id, MechanicDto mechanic);
        Task DeleteMechanic(int id);
    }
}
