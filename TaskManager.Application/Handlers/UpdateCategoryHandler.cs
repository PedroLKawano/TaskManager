using TaskManager.Application.Commands;
using TaskManager.Domain.Abstractions;
using TaskManager.Domain.Exceptions;
using TaskManager.Domain.Repositories;

namespace TaskManager.Application.Handlers;

public class UpdateCategoryHandler(ICategoryRepository repository, IUnitOfWork unitOfWork)
{
    private readonly ICategoryRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task HandleAsync(Guid id,
                                  UpdateCategoryCommand command,
                                  CancellationToken cancellationToken = default)
    {
        var category = await _repository.GetByIdAsync(id, cancellationToken) ??
            throw new DomainException("Categoria não encontrada.");

        category.UpdateName(command.Name);
        category.SetDescription(command.Description);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
