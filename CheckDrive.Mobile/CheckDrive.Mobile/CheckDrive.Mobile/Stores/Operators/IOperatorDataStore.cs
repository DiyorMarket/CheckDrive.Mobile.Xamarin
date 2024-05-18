using CheckDrive.DTOs.Operator;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CheckDrive.Web.Stores.Operators
{
    public interface IOperatorDataStore
    {
        Task<List<OperatorDto>> GetOperators();
        Task<OperatorDto> GetOperator(int id);
        Task<OperatorDto> CreateOperator(OperatorDto @operator);
        Task<OperatorDto> UpdateOperator(int id, OperatorDto @operator);
        Task DeleteOperator(int id);
    }
}
    