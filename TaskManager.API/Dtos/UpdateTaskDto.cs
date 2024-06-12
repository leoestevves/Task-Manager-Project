namespace TaskManager.API.Dtos;

public record class UpdateTaskDto(
    string Title,
    string Description
);