using CheckDrive.DTOs.Dispatcher;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckDrive.Web.Stores.Dispatchers
{
    public class MockDispatcherDataStore : IDispatcherDataStore
    {
        private readonly List<DispatcherDto> _dispatchers;

        public MockDispatcherDataStore()
        {
            _dispatchers = new List<DispatcherDto>
            {
                new DispatcherDto { Id = 1, AccountId = 1 },
                new DispatcherDto { Id = 2, AccountId = 2 },
            };
        }

        public async Task<List<DispatcherDto>> GetDispatchers()
        {
            await Task.Delay(100);
            return _dispatchers.ToList();
        }

        public async Task<DispatcherDto> GetDispatcher(int id)
        {
            await Task.Delay(100);
            return _dispatchers.FirstOrDefault(d => d.Id == id);
        }

        public async Task<DispatcherDto> CreateDispatcher(DispatcherDto dispatcher)
        {
            await Task.Delay(100);
            dispatcher.Id = _dispatchers.Max(d => d.Id) + 1;
            _dispatchers.Add(dispatcher);
            return dispatcher;
        }

        public async Task<DispatcherDto> UpdateDispatcher(int id, DispatcherDto dispatcher)
        {
            await Task.Delay(100);
            var existingDispatcher = _dispatchers.FirstOrDefault(d => d.Id == id);
            if (existingDispatcher != null)
            {
                existingDispatcher.AccountId = dispatcher.AccountId;
            }
            return existingDispatcher;
        }

        public async Task DeleteDispatcher(int id)
        {
            await Task.Delay(100);
            var dispatcherToRemove = _dispatchers.FirstOrDefault(d => d.Id == id);
            if (dispatcherToRemove != null)
            {
                _dispatchers.Remove(dispatcherToRemove);
            }
        }
    }
}
