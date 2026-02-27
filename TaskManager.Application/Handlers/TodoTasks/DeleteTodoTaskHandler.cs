using TaskManager.Domain.Abstractions;
using TaskManager.Domain.Exceptions;
using TaskManager.Domain.Repositories;

namespace TaskManager.Application.Handlers.TodoTasks;

public class DeleteTodoTaskHandler(ITodoTaskRepository repository, IUnitOfWork unitOfWork)
{
    private readonly ITodoTaskRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task HandleAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var task = await _repository.GetByIdAsync(id, cancellationToken) ??
            throw new DomainException("Tarefa não encontrada.");

        _repository.Remove(task);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
