using Microsoft.EntityFrameworkCore;
using TaskManager.API.Entities;

namespace TaskManager.API.Data;

public class TaskManagerContext(DbContextOptions<TaskManagerContext> options) : DbContext(options)
{
    public DbSet<TaskEntity> Tasks => Set<TaskEntity>();
}
