using TaskManager.Application.DTOs;
using TaskManager.Domain.Repositories;

namespace TaskManager.Application.Queries;

public class GetAllCategoriesQueryHandler(ICategoryRepository repository)
{
    private readonly ICategoryRepository _repository = repository;

    public async Task<IEnumerable<CategoryDto>> HandleAsync(CancellationToken cancellationToken = default)
    {
        var categories = await _repository.GetAllAsync(cancellationToken);

        return categories.Select(category => new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description
        });
    }
}
