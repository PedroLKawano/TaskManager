using TaskManager.Domain.Abstractions;
using TaskManager.Domain.Commands;
using TaskManager.Domain.Exceptions;
using TaskManager.Domain.Repositories;

namespace TaskManager.Application.Handlers;

public class StartTodoTaskHandler(ITodoTaskRepository repository, IUnitOfWork unitOfWork)
{
    private readonly ITodoTaskRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task HandleAsync(StartTodoTaskCommand command,
                                  CancellationToken cancellationToken = default)
    {
        var task = await _repository.GetByIdAsync(command.TodoTaskId, cancellationToken) ??
            throw new DomainException("Tarefa não encontrada.");
        
        task.Start();

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
