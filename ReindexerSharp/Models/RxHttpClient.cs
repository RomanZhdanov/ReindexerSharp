using Newtonsoft.Json;
using ReindexerClient.RxCore.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ReindexerClient.Models
{
    internal class RxHttpClient
    {
        private readonly HttpClient _httpClient;

        public RxHttpClient(string url)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(url);
        }

        public async Task<T> GetAsync<T>(string url)
        {
            using var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                var status = await DeserializeResponseContentAsync<StatusResponse>(response);

                throw new Exception($"Неудачный запрос {response.RequestMessage}. Ответ: {status.ResponseCode}, {status.Description}");
            }

            return await DeserializeResponseContentAsync<T>(response);
        }
                
        public async Task<T> PostAsync<T>(string url, StringContent json)
        {
            using var response = await _httpClient.PostAsync(url, json);
            
            if (!response.IsSuccessStatusCode)
            {
                var status = await DeserializeResponseContentAsync<StatusResponse>(response);

                throw new Exception($"Неудачный запрос {response.RequestMessage}. Ответ: {status.ResponseCode}, {status.Description}");
            }

            return await DeserializeResponseContentAsync<T>(response);
        }

        public async Task<T> PutAsync<T>(string url, StringContent content)
        {
            using var response = await _httpClient.PutAsync(url, content);

            if (!response.IsSuccessStatusCode)
            {
                var status = await DeserializeResponseContentAsync<StatusResponse>(response);

                throw new Exception($"Неудачный запрос {response.RequestMessage}. Ответ: {status.ResponseCode}, {status.Description}");
            }

            return await DeserializeResponseContentAsync<T>(response);
        }

        public async Task<T> PatchAsync<T>(string url, StringContent content)
        {
            using var response = await _httpClient.PatchAsync(url, content);
            
            if (!response.IsSuccessStatusCode)
            {
                var status = await DeserializeResponseContentAsync<StatusResponse>(response);

                throw new Exception($"Неудачный запрос {response.RequestMessage}. Ответ: {status.ResponseCode}, {status.Description}");
            }

            return await DeserializeResponseContentAsync<T>(response);
        }

        public async Task<T> DeleteAsync<T>(string url)
        {
            using var response = await _httpClient.DeleteAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                var status = await DeserializeResponseContentAsync<StatusResponse>(response);

                throw new Exception($"Неудачный запрос {response.RequestMessage}. Ответ: {status.ResponseCode}, {status.Description}");
            }

            return await DeserializeResponseContentAsync<T>(response);
        }

        public async Task<T> DeleteAsync<T>(string url, StringContent content)
        {
            var deleteMessage = new HttpRequestMessage(HttpMethod.Delete, url)
            {
                Content = content
            };

            using var response = await _httpClient.SendAsync(deleteMessage);

            if (!response.IsSuccessStatusCode)
            {
                var status = await DeserializeResponseContentAsync<StatusResponse>(response);

                throw new Exception($"Неудачный запрос {response.RequestMessage}. Ответ: {status.ResponseCode}, {status.Description}");
            }

            return await DeserializeResponseContentAsync<T>(response);
        }

        private async Task<T> DeserializeResponseContentAsync<T>(HttpResponseMessage response)
        {
            using var content = response.Content;
            var result = await content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(result);
        }
    }
}
