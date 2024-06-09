using CheckDrive.ApiContracts.MechanicAcceptance;
using CheckDrive.Mobile.Responses;
using System.Collections.Generic;

namespace CheckDrive.Web.Stores.MechanicAcceptances
{
    public interface IMechanicAcceptanceDataStore
    {
        GetMechanicAcceptanceResponse GetMechanicAcceptances();
        MechanicAcceptanceDto GetMechanicAcceptance(int id);
        MechanicAcceptanceDto CreateMechanicAcceptance(MechanicAcceptanceForCreateDto mechanicAcceptance);
        MechanicAcceptanceDto UpdateMechanicAcceptance(int id, MechanicAcceptanceForUpdateDto mechanicAcceptance);
        void DeleteMechanicAcceptance(int id);
    }
}
