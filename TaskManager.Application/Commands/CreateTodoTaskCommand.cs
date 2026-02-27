using TaskManager.Domain.Enums;

namespace TaskManager.Application.Commands;

public record CreateTodoTaskCommand(string Title,
                                    Priority Priority,
                                    string? Description,
                                    Guid CategoryId);
