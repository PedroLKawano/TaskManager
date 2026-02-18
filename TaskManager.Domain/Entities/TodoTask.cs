using TaskManager.Domain.Enums;
using TaskManager.Domain.Exceptions;

namespace TaskManager.Domain.Entities;

public class TodoTask
{
    private TodoTask() { }

    public TodoTask(string title, Priority priority, string description, Category category)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new DomainException("O título é obrigatório.");

        if (title.Length > 50)
            throw new DomainException("O título pode conter no máximo 50 caracteres.");

        if (priority == Priority.Urgent && string.IsNullOrWhiteSpace(description))
            throw new DomainException("A descrição é obrigatória em tarefas urgentes.");

        if (description?.Length > 150)
            throw new DomainException("A descrição pode conter no máximo 150 caracteres");

        if (category is null)
            throw new DomainException("A categoria é obrigatória.");

        Title = title;
        Priority = priority;
        Description = description ?? string.Empty;
        CategoryId = category.Id;
        Category = category;
    }

    public Guid Id { get; private set; }
    public string Title { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public Guid CategoryId { get; private set; }
    public Category Category { get; private set; } = null!;
    public DateTime CreationDate { get; private set; } = DateTime.UtcNow;
    public DateTime? ConclusionDate { get; private set; }
    public Status Status { get; private set; } = Status.Pending;
    public Priority Priority { get; private set; }

    public void Start()
    {
        if (Status != Status.Pending)
            throw new DomainException("Só é possível iniciar uma tarefa pendente.");

        Status = Status.InProgress;
    }

    public void Complete()
    {
        if (Status != Status.InProgress)
            throw new DomainException("Só é possível completar uma tarefa em progresso.");

        ConclusionDate = DateTime.UtcNow;
        Status = Status.Completed;
    }

    public void Cancel()
    {
        if (Status == Status.Completed)
            throw new DomainException("Não é possível cancelar uma tarefa completada.");

        Status = Status.Canceled;
    }
}
