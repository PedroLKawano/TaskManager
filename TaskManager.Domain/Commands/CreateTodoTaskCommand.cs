using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Commands;

public record CreateTodoTaskCommand(string Title,
                                    Priority Priority,
                                    string Description,
                                    Guid CategoryId);
