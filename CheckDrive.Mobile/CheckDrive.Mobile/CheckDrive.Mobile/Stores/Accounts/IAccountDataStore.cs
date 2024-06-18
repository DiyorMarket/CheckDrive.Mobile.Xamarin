using CheckDrive.ApiContracts.Account;
using CheckDrive.Mobile.Responses;
using System.Threading.Tasks;

namespace CheckDrive.Web.Stores.Accounts
{
    public interface IAccountDataStore
    {
        string CreateTokenAsync(string login, string password);
        GetAccountResponse GetAccountsAsync(string login);
        AccountDto GetAccountAsync(int id);
        AccountDto CreateAccountAsync(AccountDto account);
    }
}
