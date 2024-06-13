using System.ComponentModel.DataAnnotations;

namespace TaskManager.API.Dtos;

public record class UpdateTaskDto(
    [Required][StringLength(50)] string Title,
    [Required][StringLength(200)] string Description
);