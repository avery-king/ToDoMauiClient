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

    public Task<List<ToDo>> GetAllItemsAsync()
    {
        throw new NotImplementedException();
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