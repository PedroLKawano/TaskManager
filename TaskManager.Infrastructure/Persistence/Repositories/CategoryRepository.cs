using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Repositories;

namespace TaskManager.Infrastructure.Persistence.Repositories;

public class CategoryRepository(TaskManagerDbContext context)
    : ICategoryRepository
{
    private readonly TaskManagerDbContext _context = context;

    public async Task AddAsync(Category category, CancellationToken cancellationToken = default)
        => await _context.Categories.AddAsync(category, cancellationToken);

    public async Task<Category?> GetByIdAsync(Guid id,
                                              CancellationToken cancellationToken = default)
    {
        return await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }
    
    public async Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken = default)
        => await _context.Categories.ToListAsync(cancellationToken);

    public void Remove(Category category)
        => _context.Categories.Remove(category);
}
