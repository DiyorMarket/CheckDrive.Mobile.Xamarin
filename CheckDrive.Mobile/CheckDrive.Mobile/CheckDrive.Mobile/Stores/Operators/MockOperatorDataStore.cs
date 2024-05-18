using CheckDrive.ApiContracts.Operator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckDrive.Web.Stores.Operators
{
    public class MockOperatorDataStore : IOperatorDataStore
    {
        private readonly List<OperatorDto> _operators;

        public MockOperatorDataStore()
        {
            _operators = new List<OperatorDto>
            {
                new OperatorDto { Id = 1, Login = "user1", Password = "password1", PhoneNumber = "123456789", FirstName = "John", LastName = "Doe", Birthdate = new DateTime(1990, 1, 1) },
                new OperatorDto { Id = 2, Login = "user2", Password = "password2", PhoneNumber = "987654321", FirstName = "Jane", LastName = "Siu", Birthdate = new DateTime(1995, 5, 15)},
            };
        }

        public async Task<List<OperatorDto>> GetOperators()
        {
            await Task.Delay(100);
            return _operators.ToList();
        }

        public async Task<OperatorDto> GetOperator(int id)
        {
            await Task.Delay(100);
            return _operators.FirstOrDefault(op => op.Id == id);
        }

        public async Task<OperatorDto> CreateOperator(OperatorDto @operator)
        {
            await Task.Delay(100);
            @operator.Id = _operators.Max(op => op.Id) + 1;
            _operators.Add(@operator);
            return @operator;
        }

        public async Task DeleteOperator(int id)
        {
            await Task.Delay(100);
            var operatorToRemove = _operators.FirstOrDefault(op => op.Id == id);
            if (operatorToRemove != null)
            {
                _operators.Remove(operatorToRemove);
            }
        }
    }
}
