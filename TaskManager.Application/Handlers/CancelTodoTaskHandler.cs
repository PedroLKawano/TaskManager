using TaskManager.Domain.Commands;
using TaskManager.Domain.Exceptions;
using TaskManager.Domain.Repositories;

namespace TaskManager.Application.Handlers
{
    public class CancelTodoTaskHandler(ITodoTaskRepository repository)
    {
        private readonly ITodoTaskRepository _repository = repository;

        public void Handle(CancelTodoTaskCommand command)
        {
            var task = _repository.GetById(command.TodoTaskId) ??
                throw new DomainException("Tarefa não encontrada.");

            task.Cancel();

            _repository.Update(task);
        }
    }
}
