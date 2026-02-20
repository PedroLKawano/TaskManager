using TaskManager.Application.Commands;
using TaskManager.Domain.Abstractions;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Repositories;

namespace TaskManager.Application.Handlers;

public class CreateCategoryHandler(ICategoryRepository repository, IUnitOfWork unitOfWork)
{
    private readonly ICategoryRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Guid> HandleAsync(CreateCategoryCommand command,
                                        CancellationToken cancellationToken = default)
    {
        var category = new Category(command.Name);

        category.SetDescription(command.Description);

        await _repository.AddAsync(category, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return category.Id;
    }
}
