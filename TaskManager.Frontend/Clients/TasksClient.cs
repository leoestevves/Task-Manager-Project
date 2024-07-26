using System.Collections.Immutable;
using TaskManager.Frontend.Models;

namespace TaskManager.Frontend.Clients;

public class TasksClient(HttpClient httpClient)
{
    private readonly List<TaskSummary> tasks = 
    [
        new ()
        {
            Id = 1,
            Title = "Limpar a casa",
            Description = "Aproveitar o final de semana para limpar a casa inteira."
        },
        new ()
        {
            Id = 2,
            Title = "Ir ao mercado",
            Description = "Fazer compras pela manhã no mercado mais próximo."
        },
        new ()
        {
            Id = 3,
            Title = "Finalizar projeto pessoal.",
            Description = "Terminar o projeto de games nesse final de semana."
        }
    ];

    public TaskSummary[] GetTasks() => tasks.ToArray();

    public void AddTask(TaskDetails task)
    {
        var taskSummary = new TaskSummary
        {
            Id = tasks.Count + 1,
            Title = task.Title,
            Description = task.Description
        };

        tasks.Add(taskSummary);
    }

    public TaskDetails GetTask(int id)
    {
        TaskSummary task = GetTaskSummaryById(id);

        return new TaskDetails
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description
        };
    }

    public void UpdateTask(TaskDetails updatedTask)
    {
        TaskSummary existingTask = GetTaskSummaryById(updatedTask.Id);

        existingTask.Title = updatedTask.Title;
        existingTask.Description = updatedTask.Description;
    }

    public void DeleteTask(int id)
    {
        var task = GetTaskSummaryById(id);
        tasks.Remove(task);
    }

    private TaskSummary GetTaskSummaryById(int id)
    {
        TaskSummary? task = tasks.Find(task => task.Id == id);
        ArgumentNullException.ThrowIfNull(task);
        return task;
    }
}
