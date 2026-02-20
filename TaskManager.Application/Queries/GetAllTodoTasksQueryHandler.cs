using TaskManager.Application.DTOs;
using TaskManager.Domain.Repositories;

namespace TaskManager.Application.Queries;

public class GetAllTodoTasksQueryHandler(ITodoTaskRepository repository)
{
    private readonly ITodoTaskRepository _repository = repository;

    public async Task<IEnumerable<TodoTaskDto>> HandleAsync(CancellationToken cancellationToken = default)
    {
        var tasks = await _repository.GetAllAsync(cancellationToken);

        return tasks.Select(task => new TodoTaskDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            CategoryName = task.Category.Name,
            Status = task.Status.ToString(),
            Priority = task.Priority.ToString(),
            CreationDate = task.CreationDate,
            ConclusionDate = task.ConclusionDate
        });
    }
}
