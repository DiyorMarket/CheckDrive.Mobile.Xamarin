using CheckDrive.DTOs.MechanicAcceptance;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CheckDrive.Web.Stores.MechanicAcceptances
{
    public interface IMechanicAcceptanceDataStore
    {
        Task<List<MechanicAcceptanceDto>> GetMechanicAcceptances();
        Task<MechanicAcceptanceDto> GetMechanicAcceptance(int id);
        Task<MechanicAcceptanceDto> CreateMechanicAcceptance(MechanicAcceptanceDto mechanicAcceptance);
        Task<MechanicAcceptanceDto> UpdateMechanicAcceptance(int id, MechanicAcceptanceDto mechanicAcceptance);
        Task DeleteMechanicAcceptance(int id);
    }
}
