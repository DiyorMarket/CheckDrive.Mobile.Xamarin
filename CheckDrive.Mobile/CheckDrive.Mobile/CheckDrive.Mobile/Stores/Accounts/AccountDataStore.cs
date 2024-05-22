using CheckDrive.ApiContracts.Account;
using CheckDrive.Mobile.Responses;
using CheckDrive.Mobile.Services;
using CheckDrive.Web.Stores.Accounts;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace CheckDrive.Mobile.Stores.Accounts
{
    public class AccountDataStore : IAccountDataStore
    {
        private readonly ApiClient _api;

        public AccountDataStore(ApiClient apiClient)
        {
            _api = apiClient;
        }

        public async Task<GetAccountResponse> GetAccounts(int roleId)
        {
            StringBuilder query = new StringBuilder("");

            if (roleId != null)
            {
                query.Append($"roleId={roleId}&");
            }

            var response = _api.GetAsync("accounts?" + query.ToString());
            if(!response.Result.IsSuccessStatusCode)
            {
                throw new Exception("Could not fetch accounts.");
            }

            var json = await response.Result.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<GetAccountResponse>(json);

            return result;
        }

        public async Task<AccountDto> GetAccount(int id)
        {
            var response = _api.GetAsync($"accounts/{id}").Result;

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Could not fetch accounts with id: {id}.");
            }

            var json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var result = JsonConvert.DeserializeObject<AccountDto>(json);

            return result;
        }

        public async Task<AccountDto> CreateAccount(AccountDto account)
        {
            var json = JsonConvert.SerializeObject(account);
            var response = _api.PostAsync("accounts", json).Result;

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error creating accounts.");
            }

            var jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            return JsonConvert.DeserializeObject<AccountDto>(jsonResponse);
        }

        public async Task<AccountDto> UpdateAccount(int id, AccountDto account)
        {
            var json = JsonConvert.SerializeObject(account);
            var response = _api.PutAsync($"accounts/{account.Id}", json).Result;

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error updating accounts.");
            }

            var jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            return JsonConvert.DeserializeObject<AccountDto>(jsonResponse);
        }

        public async Task DeleteAccount(int id)
        {
            var response = _api.DeleteAsync($"accounts/{id}").Result;

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Could not delete accounts with id: {id}.");
            }
        }
    }
}
