using TaskFlow.Domain.Enums;

namespace TaskFlow.Domain.Entities
{
    public class TaskItem
    {
        public Guid Id { get; set; }
        public string Title { get; private set; } = string.Empty;
        public string? Description { get; set; }
        public WorkItemStatus Status { get; private set; }
        public int Priority { get; set; }
        public Guid ProjectId { get; set; }
        public string? AssignedToUserId { get; set; }
        public DateTime CreatedAt { get; set; }

        private TaskItem() { }

        public TaskItem(string title, Guid projectId)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty");

            Id = Guid.NewGuid();
            Title = title;
            ProjectId = projectId;
            Status = WorkItemStatus.Todo;
            CreatedAt = DateTime.UtcNow;
        }

        public void Rename(string newTitle)
        {
            if (string.IsNullOrWhiteSpace(newTitle))
                throw new ArgumentException("Title cannot be empty");

            Title = newTitle;
        }

        public void MoveTo(WorkItemStatus newStatus)
        {
            Status = newStatus;
        }
    }
}