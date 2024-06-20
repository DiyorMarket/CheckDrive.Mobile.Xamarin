using CheckDrive.ApiContracts.Account;
using CheckDrive.Mobile.Responses;
using CheckDrive.Mobile.Services;
using CheckDrive.Web.Stores.Accounts;
using Newtonsoft.Json;
using System;
using System.Net.Http;
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

        

        public async Task<string> CreateTokenAsync(string login, string password)
        {
            var json = JsonConvert.SerializeObject(new { login, password });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _api.PostAsync("login/login", json);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Could not fetch token.");
            }

            var tokenJson = await response.Content.ReadAsStringAsync();
            var token = JsonConvert.DeserializeObject<string>(tokenJson);

            return token;
        }

        
    }
}
