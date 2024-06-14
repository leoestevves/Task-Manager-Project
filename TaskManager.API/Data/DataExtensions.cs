using Microsoft.EntityFrameworkCore;

namespace TaskManager.API.Data;

public static class DataExtensions
{
    public static void MigrateDb(this WebApplication app) //Sempre cria o db quando inicia
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TaskManagerContext>();
        dbContext.Database.Migrate();
    }
}
