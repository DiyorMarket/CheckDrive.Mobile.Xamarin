using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace CheckDrive.Mobile.Services
{
    public class ApiClient
    {
        private const string BaseUrl = "https://2bvq12nl-7111.euw.devtunnels.ms/api";

        private readonly HttpClient _client;

        public ApiClient()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(BaseUrl);
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async Task<HttpResponseMessage> GetAsync(string resource, bool isFullUrl = false)
        {
            string url = isFullUrl ? resource : BaseUrl + "/" + resource;

            try
            {
                var token = await SecureStorage.GetAsync("tasty-cookies");

                if (string.IsNullOrEmpty(token))
                {
                    throw new Exception("Token is empty.");
                }

                var request = new HttpRequestMessage(HttpMethod.Get, new Uri(_client.BaseAddress, url));
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response =  _client.SendAsync(request).Result;

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Failed to get data from {resource}. Status code: {response.StatusCode}");
                }

                return response;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP request failed: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        public async Task<HttpResponseMessage> PostAsync(string resource, string body)
        {
            try
            {
                string token = await SecureStorage.GetAsync("tasty-cookies");

                var request = new HttpRequestMessage(HttpMethod.Post, $"{BaseUrl}/{resource}");
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                request.Content = new StringContent(body, Encoding.UTF8, "application/json");

                var response = _client.SendAsync(request).Result;

                return response;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"{ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
    }
}
