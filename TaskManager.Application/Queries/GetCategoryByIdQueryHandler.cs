using TaskManager.Application.DTOs;
using TaskManager.Domain.Repositories;

namespace TaskManager.Application.Queries;

public class GetCategoryByIdQueryHandler(ICategoryRepository repository)
{
    private readonly ICategoryRepository _repository = repository;

    public async Task<CategoryDto?> HandleAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var category = await _repository.GetByIdAsync(id, cancellationToken);

        if (category is null) return null;

        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description
        };
    }
}
