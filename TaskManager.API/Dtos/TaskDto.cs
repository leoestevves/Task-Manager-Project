namespace TaskManager.API.Dtos;

public record class TaskDto(
    int Id,
    string Title,
    string Description
);
