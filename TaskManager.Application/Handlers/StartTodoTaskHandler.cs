using TaskManager.Domain.Commands;
using TaskManager.Domain.Exceptions;
using TaskManager.Domain.Repositories;

namespace TaskManager.Application.Handlers;

public class StartTodoTaskHandler(ITodoTaskRepository repository)
{
    private readonly ITodoTaskRepository _repository = repository;

    public void Handle(StartTodoTaskCommand command)
    {
        var task = _repository.GetById(command.TodoTaskId) ??
            throw new DomainException("Tarefa não encontrada.");
        
        task.Start();

        _repository.Update(task);
    }
}
