﻿using CheckDrive.ApiContracts.Account;
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

        public GetAccountResponse GetAccountsAsync(string login)
        {
            StringBuilder query = new StringBuilder("");

            if (login != null)
            {
                query.Append("Login=" + login.Trim());
            }

            var response =  _api.GetAsync("accounts?" + query.ToString());
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Could not fetch accounts.");
            }

            var json =  response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<GetAccountResponse>(json);

            return result;
        }

        public string CreateTokenAsync(string login, string password)
        {
            var json = JsonConvert.SerializeObject(new { login, password });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response =  _api.PostAsync("login/login", json);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Could not fetch token.");
            }

            var tokenJson =  response.Content.ReadAsStringAsync().Result;
            var token = JsonConvert.DeserializeObject<string>(tokenJson);

            return token;
        }

        public AccountDto GetAccountAsync(int id)
        {
            var response = _api.GetAsync($"accounts/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Could not fetch accounts with id: {id}.");
            }

            var json = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<AccountDto>(json);

            return result;
        }

        public AccountDto CreateAccountAsync(AccountDto account)
        {
            var json = JsonConvert.SerializeObject(account);
            var response = _api.PostAsync("accounts", json);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error creating accounts.");
            }

            var jsonResponse = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<AccountDto>(jsonResponse);
        }
    }
}
