using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Repositories
{
    public interface ICategoryRepository
    {
        void Add(Category category);
        void Update(Category category);
        void Remove(Guid id);
        Category GetById(Guid id);
        IEnumerable<Category> GetAll();
    }
}
