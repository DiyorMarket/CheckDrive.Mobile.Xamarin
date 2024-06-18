using CheckDrive.ApiContracts.MechanicHandover;
using CheckDrive.Mobile.Responses;
using System;
using System.Threading.Tasks;

namespace CheckDrive.Web.Stores.MechanicHandovers
{
    public interface IMechanicHandoverDataStore
    {
        GetMechanicHandoverResponse GetMechanicHandoversAsync();
        GetMechanicHandoverResponse GetMechanicHandoversByDriverIdAsync(int driverId);
        GetMechanicHandoverResponse GetMechanicHandoversAsync(DateTime date);
        MechanicHandoverDto GetMechanicHandoverAsync(int id);
        MechanicHandoverDto CreateMechanicHandoverAsync(MechanicHandoverForCreateDto mechanicHandover);
    }
}
