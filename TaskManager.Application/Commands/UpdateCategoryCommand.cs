namespace TaskManager.Application.Commands;

public record UpdateCategoryCommand(Guid Id, string Name, string? Description);
