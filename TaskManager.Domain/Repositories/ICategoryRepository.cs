using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Repositories;

public interface ICategoryRepository
{
    Task AddAsync(Category category, CancellationToken cancellationToken = default);    
    Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken = default);
    void Remove(Category category);
}
