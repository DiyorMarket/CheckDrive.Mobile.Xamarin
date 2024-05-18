using CheckDrive.DTOs.MechanicHandover;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CheckDrive.Web.Stores.MechanicHandovers
{
    public interface IMechanicHandoverDataStore
    {
        Task<List<MechanicHandoverDto>> GetMechanicHandovers();
        Task<MechanicHandoverDto> GetMechanicHandover(int id);
        Task<MechanicHandoverDto> CreateMechanicHandover(MechanicHandoverDto mechanicHandover);
        Task<MechanicHandoverDto> UpdateMechanicHandover(int id, MechanicHandoverDto mechanicHandover);
        Task DeleteMechanicHandover(int id);
    }
}
