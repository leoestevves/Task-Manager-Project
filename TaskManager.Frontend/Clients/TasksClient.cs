using System.Collections.Immutable;
using TaskManager.Frontend.Models;

namespace TaskManager.Frontend.Clients;

public class TasksClient(HttpClient httpClient)
{   
    
    public async Task<TaskSummary[]> GetTasksAsync() 
        => await httpClient.GetFromJsonAsync<TaskSummary[]>("tasks") ?? []; //Caso tasks seja nulo, ele retorna um vetor vazio

    public async Task AddTaskAsync(TaskDetails task)
        => await httpClient.PostAsJsonAsync("tasks", task); //"tasks" e a rota do backend

    public async Task<TaskDetails> GetTaskAsync(int id)
        => await httpClient.GetFromJsonAsync<TaskDetails>($"tasks/{id}")
            ?? throw new Exception("Could not find task!");

    public async Task UpdateTaskAsync(TaskDetails updatedTask)
        => await httpClient.PutAsJsonAsync($"tasks/{updatedTask.Id}", updatedTask);       
    
    public async Task DeleteTaskAsync(int id)
        => await httpClient.DeleteAsync($"tasks/{id}");
    
}
