using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Entities
{
    public class Task
    {
        public Task(string title, Priority priority, string description)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new Exception("O título é obrigatório.");

            if (title.Length < 15 || title.Length > 50)
                throw new Exception("O título pode conter no máximo 50 caracteres.");

            if (priority == Priority.Urgent && string.IsNullOrWhiteSpace(description))
                throw new Exception("A descrição é obrigatória em tarefas urgentes.");

            if (description?.Length > 150)
                throw new Exception("A descrição pode conter no máximo 150 caracteres");

            Title = title;
            Priority = priority;
            Description = description ?? string.Empty;
        }

        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public Category Category { get; private set; } = new();
        public DateTime CreationDate { get; private set; } = DateTime.Now;
        public DateTime? ConclusionDate { get; private set; }
        public Status Status { get; private set; } = Status.Pending;
        public Priority Priority { get; private set; }

        public void Start()
        {
            if (Status != Status.Pending)
                throw new Exception("Só é possível iniciar uma tarefa pendente.");

            Status = Status.InProgress;
        }

        public void Complete()
        {
            if (Status != Status.InProgress)
                throw new Exception("Só é possível completar uma tarefa em progresso.");

            Status = Status.Completed;
        }

        public void Cancel()
        {
            if (Status == Status.Completed)
                throw new Exception("Não é possível cancelar uma tarefa completada.");

            Status = Status.Canceled;
        }
    }
}
