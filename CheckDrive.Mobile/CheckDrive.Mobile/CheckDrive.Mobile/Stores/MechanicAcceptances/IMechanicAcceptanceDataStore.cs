using CheckDrive.ApiContracts.MechanicAcceptance;
using CheckDrive.Mobile.Responses;
using System;
using System.Threading.Tasks;

namespace CheckDrive.Web.Stores.MechanicAcceptances
{
    public interface IMechanicAcceptanceDataStore
    {
        GetMechanicAcceptanceResponse GetMechanicAcceptancesAsync(int driverId, string sortString);
        GetMechanicAcceptanceResponse GetMechanicAcceptancesAsync(DateTime date);
        GetMechanicAcceptanceResponse GetMechanicAcceptancesAsync();
        GetMechanicAcceptanceResponse GetMechanicAcceptancesByDriverIdAsync(int driverId);
        MechanicAcceptanceDto  GetMechanicAcceptanceAsync(int id);
        MechanicAcceptanceDto CreateMechanicAcceptanceAsync(MechanicAcceptanceForCreateDto mechanicAcceptance);
    }
}
