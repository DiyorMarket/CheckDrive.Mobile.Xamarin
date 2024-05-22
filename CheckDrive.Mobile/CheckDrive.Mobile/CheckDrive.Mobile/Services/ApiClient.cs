using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CheckDrive.Mobile.Services
{
    public class ApiClient
    {
        private const string BaseUrl = "https://s4vnbqgq-7111.euw.devtunnels.ms/api";

        private readonly HttpClient _client;

        public ApiClient()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(BaseUrl);
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async Task<HttpResponseMessage> GetAsync(string resource, bool isFullUrl = false)
        {
            string url = isFullUrl ?
                resource :
                BaseUrl + "/" + resource;

            try
            {
                HttpResponseMessage response = await _client.GetAsync(url);

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
                var request = new HttpRequestMessage(HttpMethod.Post, BaseUrl + "/" + resource);
                request.Content = new StringContent(body, Encoding.UTF8, "application/json");
                var response = await _client.SendAsync(request);

                return response;
            }
            catch (HttpRequestException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<HttpResponseMessage> PutAsync(string url, string data)
        {
            var token = string.Empty;
            var request = new HttpRequestMessage(HttpMethod.Put, _client.BaseAddress?.AbsolutePath + "/" + url)
            {
                Content = new StringContent(data, Encoding.UTF8, "application/json")
            };
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await _client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException("");
            }

            return response;
        }

        public async Task<HttpResponseMessage> DeleteAsync(string url)
        {
            string token = string.Empty;
            var request = new HttpRequestMessage(HttpMethod.Delete, _client.BaseAddress?.AbsolutePath + "/" + url);
            var response = await _client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error fetching url: {url}. Status code: {response.StatusCode}");
            }

            return response;
        }
    }
}
