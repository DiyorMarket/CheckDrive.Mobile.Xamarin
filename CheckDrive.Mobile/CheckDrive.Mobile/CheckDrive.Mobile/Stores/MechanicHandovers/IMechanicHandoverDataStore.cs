using CheckDrive.ApiContracts.MechanicHandover;
using CheckDrive.Mobile.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CheckDrive.Web.Stores.MechanicHandovers
{
    public interface IMechanicHandoverDataStore
    {
        GetMechanicHandoverResponse GetMechanicHandovers();
        GetMechanicHandoverResponse GetMechanicHandoversByDriverId(int driverId);
        MechanicHandoverDto GetMechanicHandover(int id);
        MechanicHandoverDto CreateMechanicHandover(MechanicHandoverForCreateDto mechanicHandover);
        MechanicHandoverDto UpdateMechanicHandover(int id, MechanicHandoverForUpdateDto mechanicHandover);
        void DeleteMechanicHandover(int id);
    }
}
