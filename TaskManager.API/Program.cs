using TaskManager.API.Data;
using TaskManager.API.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("TaskManager");
builder.Services.AddSqlite<TaskManagerContext>(connString);

var app = builder.Build();

app.MapTasksEndpoints();
app.MigrateDb();

app.Run();
