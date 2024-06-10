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
        private const string BaseUrl = "https://srvsrv10-7111.asse.devtunnels.ms/api";
        private readonly HttpClient _client;

        public ApiClient()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(BaseUrl);
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public HttpResponseMessage Get(string resource, bool isFullUrl = false)
        {
            string url = isFullUrl ?
                resource :
                BaseUrl + "/" + resource;

            try
            {
                string token = SecureStorage.GetAsync("tasty-cookies").Result;

                var request = new HttpRequestMessage(HttpMethod.Get, _client.BaseAddress?.AbsolutePath + "/" + url);
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = _client.SendAsync(request).Result;

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

        public HttpResponseMessage Post(string resource, string body)
        {
            try
            {
                string token = SecureStorage.GetAsync("tasty-cookies").Result;

                var request = new HttpRequestMessage(HttpMethod.Post, $"{BaseUrl}/{resource}");
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                request.Content = new StringContent(body, Encoding.UTF8, "application/json");

                var response = _client.SendAsync(request).Result;

                return response;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent("An error occurred while processing the request.")
            };
        }

        public HttpResponseMessage Put(string url, string data)
        {
            string token = SecureStorage.GetAsync("tasty-cookies").Result;

            var request = new HttpRequestMessage(HttpMethod.Put, url);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            request.Content = new StringContent(data, Encoding.UTF8, "application/json");

            var response = _client.SendAsync(request).Result;

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException("");
            }

            return response;
        }

        public HttpResponseMessage Delete(string url)
        {
            string token = SecureStorage.GetAsync("tasty-cookies").Result;

            var request = new HttpRequestMessage(HttpMethod.Delete, url);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = _client.SendAsync(request).Result;

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error fetching url: {url}. Status code: {response.StatusCode}");
            }

            return response;
        }
    }
}
