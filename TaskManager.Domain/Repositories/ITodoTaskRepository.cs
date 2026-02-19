using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Repositories;

public interface ITodoTaskRepository
{
    Task AddAsync(TodoTask task, CancellationToken cancellationToken = default);
    Task<TodoTask?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<TodoTask>> GetByStatusAsync(Status status,
                                                 CancellationToken cancellationToken = default);
    void Remove(TodoTask task);
}
