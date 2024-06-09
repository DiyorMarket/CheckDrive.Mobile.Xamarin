using System;
using System.Net.Http;
using System.Text;

namespace CheckDrive.Mobile.Services
{
    public class ApiClient
    {
        private const string BaseUrl = "https://x60ngf6c-7111.euw.devtunnels.ms/api";

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
                HttpResponseMessage response =  _client.GetAsync(url).Result;

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
                var request = new HttpRequestMessage(HttpMethod.Post, BaseUrl + "/" + resource);
                request.Content = new StringContent(body, Encoding.UTF8, "application/json");
                var response =  _client.SendAsync(request).Result;

                return response;
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public HttpResponseMessage Put(string url, string data)
        {
            var token = string.Empty;
            var request = new HttpRequestMessage(HttpMethod.Put, _client.BaseAddress?.AbsolutePath + "/" + url)
            {
                Content = new StringContent(data, Encoding.UTF8, "application/json")
            };
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = _client.SendAsync(request).Result;

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException("");
            }

            return response;
        }

        public  HttpResponseMessage Delete(string url)
        {
            string token = string.Empty;
            var request = new HttpRequestMessage(HttpMethod.Delete, _client.BaseAddress?.AbsolutePath + "/" + url);
            var response = _client.SendAsync(request).Result;

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error fetching url: {url}. Status code: {response.StatusCode}");
            }

            return response;
        }
    }
}
