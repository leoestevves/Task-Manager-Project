namespace TaskManager.API.Dtos;

public record class CreateTaskDto(
    string Title,
    string Description
);
