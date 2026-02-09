using TaskManager.Domain.Commands;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Repositories;

namespace TaskManager.Application.Handlers
{
    public class CreateCategoryHandler(ICategoryRepository repository)
    {
        private readonly ICategoryRepository _repository = repository;

        public void Handle(CreateCategoryCommand command)
        {
            var category = new Category(command.Name);

            category.SetDescription(command.Description);

            _repository.Add(category);
        }
    }
}
