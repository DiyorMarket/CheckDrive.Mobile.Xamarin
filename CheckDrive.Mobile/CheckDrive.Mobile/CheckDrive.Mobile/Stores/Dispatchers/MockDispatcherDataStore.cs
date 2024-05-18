using CheckDrive.ApiContracts.Dispatcher;
using System;
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
                new DispatcherDto { Id = 1, Login = "user1", Password = "password1", PhoneNumber = "123456789", FirstName = "John", LastName = "Doe", Birthdate = new DateTime(1990, 1, 1) },
                new DispatcherDto { Id = 2, Login = "user2", Password = "password2", PhoneNumber = "987654321", FirstName = "Jane", LastName = "Siu", Birthdate = new DateTime(1995, 5, 15)},
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
