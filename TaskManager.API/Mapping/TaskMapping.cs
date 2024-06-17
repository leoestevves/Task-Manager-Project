using TaskManager.API.Dtos;
using TaskManager.API.Entities;

namespace TaskManager.API.Mapping;

public static class TaskMapping
{
    public static TaskEntity ToEntity(this CreateTaskDto task)
    {
        return new TaskEntity()
        {
            Title = task.Title,
            Description = task.Description
        };
    }

    public static TaskDto ToDto(this TaskEntity task)
    {
        return new
        (
            task.Id,
            task.Title,
            task.Description
        );
    }
}
