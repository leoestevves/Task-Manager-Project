using TaskManager.API.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GET_GAME_ENDPOINT_NAME = "GetGame"; 

List<TaskDto> tasks = 
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

//GET /tasks
app.MapGet("tasks", () => tasks);

//GET /tasks/1
app.MapGet("tasks/{id}", (int id) => tasks.Find(task => task.Id == id)).WithName(GET_GAME_ENDPOINT_NAME);

//POST /tasks
app.MapPost("tasks", (CreateTaskDto newTask) => 
{
    TaskDto task = new(
        tasks.Count + 1,
        newTask.Title,
        newTask.Description
    );

    tasks.Add(task);

    return Results.CreatedAtRoute(GET_GAME_ENDPOINT_NAME, new {id = task.Id}, task);
});

//PUT /tasks/1
app.MapPut("tasks/{id}", (int id, UpdateTaskDto updatedTask) =>
{
    var index = tasks.FindIndex(task => task.Id == id);

    tasks[index] = new TaskDto(
        id,
        updatedTask.Title,
        updatedTask.Description
    );

    return Results.NoContent();
});

//DELETE /tasks/1
app.MapDelete("tasks/{id}", (int id) => 
{
    tasks.RemoveAll(task => task.Id == id);

    return Results.NoContent();
});

app.Run();
