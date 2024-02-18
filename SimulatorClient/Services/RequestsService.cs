using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SimulatorClient.Services
{
    public class RequestsService
    {
        private static RequestsService _instance;
        public static RequestsService Instance { 
            get { 
                _instance ??= new RequestsService();
                return _instance; 
            }
            private set { }
        }
        private HttpClient _httpClient;
        public RequestsService()
        {
            _httpClient = new HttpClient();
        }
        public async Task<string> PostAsync(string uri, Object toSend)
        {
            StringContent requestContent = new StringContent(
                JsonSerializer.Serialize(toSend),
                Encoding.UTF8,
                "application/json"
            );
            HttpResponseMessage response = await _httpClient.PostAsync(uri, requestContent);
            if(!response.IsSuccessStatusCode)
                throw new HttpRequestException(await response.Content.ReadAsStringAsync());
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return jsonResponse;
        }

        public async Task<string> GetAsync(string uri)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(uri);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return jsonResponse;
        }

        public async Task<DeserializedClass> GetAsync<DeserializedClass>(string uri)
        {
            string jsonResponse = await GetAsync(uri);
            return JsonSerializer.Deserialize<DeserializedClass>(jsonResponse);
        }
        public async Task<DeserializedClass> PostAsync<DeserializedClass>(string uri, Object toSend)
        {
            string jsonResponse = await PostAsync(uri, toSend);
            return JsonSerializer.Deserialize<DeserializedClass>(jsonResponse);
        }

    }
}
