using TaskManager.Domain.Abstractions;
using TaskManager.Domain.Exceptions;
using TaskManager.Domain.Repositories;

namespace TaskManager.Application.Handlers;

public class DeleteCategoryHandler(ICategoryRepository categoryRepository,
                                   ITodoTaskRepository todoTaskRepository,
                                   IUnitOfWork unitOfWork)
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly ITodoTaskRepository _todoTaskRepository = todoTaskRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task HandleAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var category = await _categoryRepository.GetByIdAsync(id, cancellationToken) ??
            throw new DomainException("Categoria não encontrada.");

        var existsTask = await _todoTaskRepository.ExistsByCategoryIdAsync(id, cancellationToken);

        if (existsTask)
            throw new DomainException("Não é possível excluir uma categoria com tarefas vinculadas.");

        _categoryRepository.Remove(category);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
