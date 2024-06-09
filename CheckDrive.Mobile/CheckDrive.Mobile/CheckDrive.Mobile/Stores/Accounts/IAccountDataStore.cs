using CheckDrive.ApiContracts.Account;
using CheckDrive.Mobile.Responses;

namespace CheckDrive.Web.Stores.Accounts
{
    public interface IAccountDataStore
    {
        string CreateToken(string login, string password);
        GetAccountResponse GetAccounts(string login);
        AccountDto GetAccount(int id);
        AccountDto CreateAccount(AccountDto account);
        AccountDto UpdateAccount(int id, AccountDto account);
        void DeleteAccount(int id);
    }
}
