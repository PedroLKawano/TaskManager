using TaskManager.Domain.Commands;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Exceptions;
using TaskManager.Domain.Repositories;

namespace TaskManager.Application.Handlers
{
    public class CreateTodoTaskHandler(ITodoTaskRepository taskRepository,
                                       ICategoryRepository categoryRepository)
    {
        private readonly ITodoTaskRepository _taskRepository = taskRepository;
        private readonly ICategoryRepository _categoryRepository = categoryRepository;

        public void Handle(CreateTodoTaskCommand command)
        {
            var category = _categoryRepository.GetById(command.CategoryId) ??
                throw new DomainException("Categoria da tarefa não encontrada.");
            
            var task = new TodoTask(command.Title,
                                    command.Priority,
                                    command.Description,
                                    category);

            _taskRepository.Add(task);
        }
    }
}
