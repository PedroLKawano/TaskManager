using TaskManager.Application.Commands;
using TaskManager.Domain.Abstractions;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Exceptions;
using TaskManager.Domain.Repositories;

namespace TaskManager.Application.Handlers;

public class CreateTodoTaskHandler(ITodoTaskRepository taskRepository,
                                   ICategoryRepository categoryRepository,
                                   IUnitOfWork unitOfWork)
{
    private readonly ITodoTaskRepository _taskRepository = taskRepository;
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Guid> HandleAsync(CreateTodoTaskCommand command,
                                        CancellationToken cancellationToken = default)
    {
        var category = await _categoryRepository.GetByIdAsync(command.CategoryId, cancellationToken) ??
            throw new DomainException("Categoria da tarefa não encontrada.");
        
        var task = new TodoTask(command.Title,
                                command.Priority,
                                command.Description,
                                category);

        await _taskRepository.AddAsync(task, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return task.Id;
    }
}
