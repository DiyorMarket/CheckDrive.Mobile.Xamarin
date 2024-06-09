using CheckDrive.ApiContracts.Dispatcher;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace CheckDrive.Web.Stores.Dispatchers
{
    public interface IDispatcherDataStore
    {
        Task<List<DispatcherDto>> GetDispatchers();
        Task<DispatcherDto> GetDispatcher(int id);
        Task<DispatcherDto> CreateDispatcher(DispatcherDto dispatcher);
        Task DeleteDispatcher(int id);
    }
}
