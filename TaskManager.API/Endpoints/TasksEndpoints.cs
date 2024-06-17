using Microsoft.EntityFrameworkCore;
using TaskManager.API.Data;
using TaskManager.API.Dtos;
using TaskManager.API.Entities;
using TaskManager.API.Mapping;

namespace TaskManager.API.Endpoints;

public static class TasksEndpoints
{
    const string GET_GAME_ENDPOINT_NAME = "GetGame";

    public static RouteGroupBuilder MapTasksEndpoints(this WebApplication app) //RouteGroupBuilder substituindo o WebApplication
    {
        var group = app.MapGroup("tasks") //Com isso nao precisa ficar escrevendo "tasks" como string em cada metodo do CRUD, substituindo "app" por "group".
                       .WithParameterValidation(); //Metodo importado do MinimalApis.Extension

        //(Read)  GET /tasks
        group.MapGet("/", (TaskManagerContext dbContext) =>         
            dbContext.Tasks
                     .Select(task => task.ToDto())
                     .AsNoTracking()); //Otimizacao
        

        //(Read)  GET /tasks/1
        group.MapGet("/{id}", (int id, TaskManagerContext dbContext) =>
        {
            TaskEntity? task = dbContext.Tasks.Find(id);

            if (task == null)
            {
                return Results.NotFound();
            }
            else
            {
                return Results.Ok(task.ToDto());
            }
        })
        .WithName(GET_GAME_ENDPOINT_NAME);

        //(Create)  POST /tasks
        group.MapPost("/", (CreateTaskDto newTask, TaskManagerContext dbContext) =>
        {
            TaskEntity task = newTask.ToEntity();

            dbContext.Tasks.Add(task);
            dbContext.SaveChanges();

            return Results.CreatedAtRoute(GET_GAME_ENDPOINT_NAME, new { id = task.Id }, task.ToDto());
        });


        //(Update)  PUT /tasks/1
        group.MapPut("/{id}", (int id, UpdateTaskDto updatedTask, TaskManagerContext dbContext) =>
        {
            var existingTask = dbContext.Tasks.Find(id);

            if (existingTask is null) //Se nao encontrar, retorna NotFound
            {
                return Results.NotFound();
            }

            dbContext.Entry(existingTask).CurrentValues.SetValues(updatedTask.ToEntity(id));

            dbContext.SaveChanges();

            return Results.NoContent();
        });

        //(Delete)  DELETE /tasks/1
        group.MapDelete("/{id}", (int id, TaskManagerContext dbContext) =>
        {
            dbContext.Tasks
                     .Where(task => task.Id == id)
                     .ExecuteDelete();

            return Results.NoContent();
        });

        return group;
    }
}
