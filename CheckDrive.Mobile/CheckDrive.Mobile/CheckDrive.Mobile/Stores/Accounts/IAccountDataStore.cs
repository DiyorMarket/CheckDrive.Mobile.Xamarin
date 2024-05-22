using CheckDrive.ApiContracts.Account;
using CheckDrive.Mobile.Responses;
using System.Threading.Tasks;

namespace CheckDrive.Web.Stores.Accounts
{
    public interface IAccountDataStore
    {
        Task<GetAccountResponse> GetAccounts(int roleId);
        Task<AccountDto> GetAccount(int id);
        Task<AccountDto> CreateAccount(AccountDto account);
        Task<AccountDto> UpdateAccount(int id, AccountDto account);
        Task DeleteAccount(int id);
    }
}
