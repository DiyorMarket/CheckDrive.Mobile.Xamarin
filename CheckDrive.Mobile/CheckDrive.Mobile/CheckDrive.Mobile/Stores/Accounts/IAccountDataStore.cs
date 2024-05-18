using CheckDrive.DTOs.Account;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CheckDrive.Web.Stores.Accounts
{
    public interface IAccountDataStore
    {
        Task<List<AccountDto>> GetAccounts();
        Task<AccountDto> GetAccount(int id);
        Task<AccountDto> CreateAccount(AccountDto account);
        Task<AccountDto> UpdateAccount(int id, AccountDto account);
        Task DeleteAccount(int id);
    }
}
