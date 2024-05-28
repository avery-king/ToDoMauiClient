using System.Diagnostics;
using System.Text.Json;
using ToDoMauiClient.Models;

namespace ToDoMauiClient.Data;

public class RestDataService : IRestDataService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseAddress;
    private readonly string _url;
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    
    public RestDataService()
    {
        _httpClient = new HttpClient();
        _baseAddress = DeviceInfo.Platform == DevicePlatform.Android
            ? "http://10.0.2.2:5209"
            : "https://localhost:7209";
        _url = $"{_baseAddress}/api";
        _jsonSerializerOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    public async Task<List<ToDo>?> GetAllItemsAsync()
    {
        var toDoList = new List<ToDo>();

        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
        {
            Debug.WriteLine("----> No internet access");
            return toDoList;
        }

        try
        {
            var response = await _httpClient.GetAsync($"{_url}/api/todo");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                toDoList = JsonSerializer.Deserialize<List<ToDo>>(content, _jsonSerializerOptions);
            }
            else
            {
                Debug.WriteLine("----> Unsuccessful HTTP  code");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Exception: {ex.Message}");
        }

        return toDoList;
    } 

    public Task AddItemAsync(ToDo toDo)
    {
        throw new NotImplementedException();
    }

    public Task UpdateItemAsync(ToDo toDo)
    {
        throw new NotImplementedException();
    }

    public Task DeleteItemAsync(int id)
    {
        throw new NotImplementedException();
    }
}