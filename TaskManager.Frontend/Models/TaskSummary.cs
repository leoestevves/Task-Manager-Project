namespace TaskManager.Frontend.Models;

public class TaskSummary
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
}
