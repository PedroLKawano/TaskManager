using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Repositories;

public interface ITodoTaskRepository
{
    void Add(TodoTask task);
    void Update(TodoTask task);
    void Remove(Guid id);
    TodoTask GetById(Guid id);
    IEnumerable<TodoTask> GetByStatus(Status status);
}
