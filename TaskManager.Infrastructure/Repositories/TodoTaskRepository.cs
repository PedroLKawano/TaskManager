using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Repositories;
using TaskManager.Infrastructure.Persistence;

namespace TaskManager.Infrastructure.Repositories;

public class TodoTaskRepository(TaskManagerDbContext context)
    : ITodoTaskRepository
{
    private readonly TaskManagerDbContext _context = context;

    public async Task AddAsync(TodoTask task)
        => await _context.AddAsync(task);

    public async Task<TodoTask?> GetByIdAsync(Guid id)
        => await _context.TodoTasks
        .Include(t => t.Category)
        .FirstOrDefaultAsync(t => t.Id == id);

    public async Task<IEnumerable<TodoTask>> GetByStatusAsync(Status status)
        => await _context.TodoTasks
        .Include(t => t.Category)
        .Where(t => t.Status == status)
        .ToListAsync();

    public void Remove(TodoTask task)
        => _context.TodoTasks.Remove(task);
}
