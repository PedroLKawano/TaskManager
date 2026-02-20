using TaskManager.Application.DTOs;
using TaskManager.Domain.Repositories;

namespace TaskManager.Application.Queries;

public class GetTodoTaskByIdQueryHandler(ITodoTaskRepository repository)
{
    private readonly ITodoTaskRepository _repository = repository;

    public async Task<TodoTaskDto?> HandleAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var task = await _repository.GetByIdAsync(id, cancellationToken);

        if (task is null) return null;

        return new TodoTaskDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            CategoryName = task.Category.Name,
            Status = task.Status.ToString(),
            Priority = task.Priority.ToString(),
            CreationDate = task.CreationDate,
            ConclusionDate = task.ConclusionDate
        };
    }
}
