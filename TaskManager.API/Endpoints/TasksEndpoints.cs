using TaskManager.API.Dtos;

namespace TaskManager.API.Endpoints;

public static class TasksEndpoints
{
    const string GET_GAME_ENDPOINT_NAME = "GetGame";

    private static readonly List<TaskDto> tasks =
    [
        new (
        1,
        "Limpar a casa.",
        "Aproveitar o final de semana para limpar a casa inteira."),
    new (
        2,
        "Ir ao mercado.",
        "Fazer compras pela manhã no mercado mais próximo."),
    new (
        3,
        "Finalizar projeto pessoal.",
        "Terminar o projeto de games nesse final de semana.")
    ];

    public static RouteGroupBuilder MapTasksEndpoints(this WebApplication app) //RouteGroupBuilder substituindo o WebApplication
    {       
        var group = app.MapGroup("tasks") //Com isso nao precisa ficar escrevendo "tasks" como string em cada metodo do CRUD, substituindo "app" por "group".
                       .WithParameterValidation(); //Metodo importado do MinimalApis.Extension

        //(Read)  GET /tasks
        group.MapGet("/", () => tasks);

        //(Read)  GET /tasks/1
        group.MapGet("/{id}", (int id) =>
        {
            TaskDto? task = tasks.Find(task => task.Id == id);

            if (task == null)
            {
                return Results.NotFound();
            }
            else
            {
                return Results.Ok(task);
            }
        })
        .WithName(GET_GAME_ENDPOINT_NAME);

        //(Create)  POST /tasks
        group.MapPost("/", (CreateTaskDto newTask) =>
        {
            TaskDto task = new(
                tasks.Count + 1,
                newTask.Title,
                newTask.Description
            );

            tasks.Add(task);

            return Results.CreatedAtRoute(GET_GAME_ENDPOINT_NAME, new { id = task.Id }, task);
        });        


        //(Update)  PUT /tasks/1
        group.MapPut("/{id}", (int id, UpdateTaskDto updatedTask) =>
        {
            var index = tasks.FindIndex(task => task.Id == id);

            if (index == -1) //Se nao encontrar, retorna NotFound
            {
                return Results.NotFound();
            }

            tasks[index] = new TaskDto(
                id,
                updatedTask.Title,
                updatedTask.Description
            );

            return Results.NoContent();
        });

        //(Delete)  DELETE /tasks/1
        group.MapDelete("/{id}", (int id) =>
        {
            tasks.RemoveAll(task => task.Id == id);

            return Results.NoContent();
        });

        return group;
    }
}
