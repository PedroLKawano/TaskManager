using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Repositories;

public interface ICategoryRepository
{
    Task AddAsync(Category category);    
    Task<Category?> GetByIdAsync(Guid id);
    Task<IEnumerable<Category>> GetAllAsync();
    void Remove(Category category);
}
