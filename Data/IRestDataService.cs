using ToDoMauiClient.Models;

namespace ToDoMauiClient.Data;

public interface IRestDataService
{
    Task<List<ToDo>> GetAllItemsAsync();
    Task AddItemAsync(ToDo toDo);
    Task UpdateItemAsync(ToDo toDo);
    Task DeleteItemAsync(int id);
}