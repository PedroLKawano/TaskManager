using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Repositories;

public interface ITodoTaskRepository
{
    Task AddAsync(TodoTask task);
    Task<TodoTask?> GetByIdAsync(Guid id);
    Task<IEnumerable<TodoTask>> GetByStatusAsync(Status status);
    void Remove(TodoTask task);
}
