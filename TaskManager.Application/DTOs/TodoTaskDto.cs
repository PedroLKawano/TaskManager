namespace TaskManager.Application.DTOs;

public class TodoTaskDto
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string CategoryName { get; init; } = string.Empty;
    public string Status { get; init; } = string.Empty;
    public string Priority { get; init; } = string.Empty;
    public DateTime CreationDate { get; init; }
    public DateTime? ConclusionDate { get; init; }
}
