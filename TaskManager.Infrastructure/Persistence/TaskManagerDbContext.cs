using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;
using TaskManager.Domain.UnitOfWork;

namespace TaskManager.Infrastructure.Persistence;

public class TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> options) :
    DbContext(options), IUnitOfWork
{
    public DbSet<TodoTask> TodoTasks { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskManagerDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
