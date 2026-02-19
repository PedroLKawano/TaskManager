using TaskManager.Domain.Exceptions;

namespace TaskManager.Domain.Entities;

public class Category
{
    private Category() { }

    public Category(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("O nome é obrigatório.");

        if (name.Length > 50)
            throw new DomainException("O nome pode conter no máximo 50 caracteres.");

        Id = Guid.NewGuid();
        Name = name;
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public string? Description { get; private set; }
    public ICollection<TodoTask> TodoTasks { get; private set; } = [];

    public void SetDescription(string? description)
    {
        if (description?.Length > 150)
            throw new DomainException("A descrição pode conter no máximo 150 caracteres");

        Description = description;
    }
}
