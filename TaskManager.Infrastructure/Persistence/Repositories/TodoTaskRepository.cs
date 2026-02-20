using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Repositories;

namespace TaskManager.Infrastructure.Persistence.Repositories;

public class TodoTaskRepository(TaskManagerDbContext context)
    : ITodoTaskRepository
{
    private readonly TaskManagerDbContext _context = context;

    public async Task AddAsync(TodoTask task, CancellationToken cancellationToken = default)
        => await _context.TodoTasks.AddAsync(task, cancellationToken);

    public async Task<TodoTask?> GetByIdAsync(Guid id,
                                              CancellationToken cancellationToken = default)
    {
        return await _context.TodoTasks
            .Include(t => t.Category)
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<TodoTask>> GetByStatusAsync(Status status,
                                                              CancellationToken cancellationToken = default)
    {
        return await _context.TodoTasks
            .Include(t=>t.Category)
            .Where(t => t.Status == status)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TodoTask>> GetAllAsync(CancellationToken cancellationToken = default)
        => await _context.TodoTasks.Include(t=>t.Category).ToListAsync(cancellationToken);

    public void Remove(TodoTask task)
        => _context.TodoTasks.Remove(task);
}
