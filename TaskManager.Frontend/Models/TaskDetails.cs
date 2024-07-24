using System.ComponentModel.DataAnnotations;

namespace TaskManager.Frontend.Models;

public class TaskDetails
{
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public required string Title { get; set; }

    [Required]
    [StringLength(150)]
    public required string Description { get; set; }
}
