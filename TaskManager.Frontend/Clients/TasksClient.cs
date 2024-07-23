using System.Collections.Immutable;
using TaskManager.Frontend.Models;

namespace TaskManager.Frontend.Clients;

public class TasksClient
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
}
