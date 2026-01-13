using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Entities
{
    public class Task
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public string Category { get; private set; } = string.Empty;
        public DateTime CreationDate { get; set; }
        public DateTime? ConclusionDate { get; private set; }
        public Status Status { get; private set; }
        public Priority Priority { get; private set; }
    }
}
