using CheckDrive.ApiContracts.Account;
using CheckDrive.Mobile.Responses;
using CheckDrive.Mobile.Services;
using CheckDrive.Web.Stores.Accounts;
using Newtonsoft.Json;
using System;
using System.Text;

namespace CheckDrive.Mobile.Stores.Accounts
{
    public class AccountDataStore : IAccountDataStore
    {
        private readonly ApiClient _api;

        public AccountDataStore(ApiClient apiClient)
        {
            _api = apiClient;
        }

        public GetAccountResponse GetAccounts(string login)
        {
            StringBuilder query = new StringBuilder("");

            if(login != null)
            {
                query.Append("Login="+login.Trim());
            }

            var response = _api.Get("accounts?" + query.ToString());
            if(!response.IsSuccessStatusCode)
            {
                throw new Exception("Could not fetch accounts.");
            }

            var json = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<GetAccountResponse>(json);

            return result;
        }

        public AccountDto GetAccount(int id)
        {
            var response = _api.Get($"accounts/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Could not fetch accounts with id: {id}.");
            }

            var json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var result = JsonConvert.DeserializeObject<AccountDto>(json);

            return result;
        }

        public AccountDto CreateAccount(AccountDto account)
        {
            var json = JsonConvert.SerializeObject(account);
            var response = _api.Post("accounts", json);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error creating accounts.");
            }

            var jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            return JsonConvert.DeserializeObject<AccountDto>(jsonResponse);
        }

        public AccountDto UpdateAccount(int id, AccountDto account)
        {
            var json = JsonConvert.SerializeObject(account);
            var response = _api.Put($"accounts/{account.Id}", json);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error updating accounts.");
            }

            var jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            return JsonConvert.DeserializeObject<AccountDto>(jsonResponse);
        }

        public void DeleteAccount(int id)
        {
            var response = _api.Delete($"accounts/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Could not delete accounts with id: {id}.");
            }
        }
    }
}
