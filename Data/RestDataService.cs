using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ToDoMauiClient.Models;

namespace ToDoMauiClient.Data
{
    public class RestDataService : IRestDataService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseAddress;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public RestDataService()
        {
            _httpClient = new HttpClient();
            _baseAddress = DeviceInfo.Platform == DevicePlatform.Android
                ? "http://10.0.2.2:5209"
                : "https://localhost:7209";
            _httpClient.BaseAddress = new Uri(_baseAddress);
            _jsonSerializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        private async Task<T> SendRequest<T>(HttpMethod method, string endpoint, object? requestBody = null)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("----> No internet access");
                return default!;
            }

            try
            {
                var request = new HttpRequestMessage(method, endpoint);
                if (requestBody != null)
                {
                    var jsonRequestBody = JsonSerializer.Serialize(requestBody, _jsonSerializerOptions);
                    request.Content = new StringContent(jsonRequestBody, Encoding.UTF8, "application/json");
                }

                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<T>(content, _jsonSerializerOptions)!;
                }
                else
                {
                    Debug.WriteLine("----> Unsuccessful HTTP code");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception: {ex.Message}");
            }

            return default!;
        }

        public async Task<List<ToDo>?> GetAllItemsAsync()
        {
            return await SendRequest<List<ToDo>>(HttpMethod.Get, "/api/todo");
        }

        public async Task AddItemAsync(ToDo toDo)
        {
            await SendRequest<object>(HttpMethod.Post, "/api/todo", toDo);
        }

        public async Task UpdateItemAsync(ToDo toDo)
        {
            await SendRequest<object>(HttpMethod.Put, $"/api/todo/{toDo.Id}", toDo);
        }

        public async Task DeleteItemAsync(int id)
        {
            await SendRequest<object>(HttpMethod.Delete, $"/api/todo/{id}");
        }
    }
}
