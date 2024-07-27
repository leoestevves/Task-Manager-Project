using TaskManager.Frontend.Clients;
using TaskManager.Frontend.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

var taskManagerApiUrl = builder.Configuration["TaskManagerApiUrl"] ?? 
    throw new Exception("TaskManagerApiUrl is not set"); //Se for nulo, retorna um erro

builder.Services.AddHttpClient<TasksClient>(client => client.BaseAddress = new Uri(taskManagerApiUrl));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

app.Run();
