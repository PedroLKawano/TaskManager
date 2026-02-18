using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Repositories;
using TaskManager.Infrastructure.Persistence;

namespace TaskManager.Infrastructure.Repositories
{
    public class CategoryRepository(TaskManagerDbContext context)
        : ICategoryRepository
    {
        private readonly TaskManagerDbContext _context = context;

        public async Task AddAsync(Category category)
            => await _context.Categories.AddAsync(category);

        public async Task<Category?> GetByIdAsync(Guid id)
            => await _context.Categories
            .Include(c => c.TodoTasks)
            .FirstOrDefaultAsync(c => c.Id == id);

        public async Task<IEnumerable<Category>> GetAllAsync()
            => await _context.Categories.ToListAsync();

        public void Remove(Category category)
            => _context.Categories.Remove(category);
    }
}
