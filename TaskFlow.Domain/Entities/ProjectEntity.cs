using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskFlow.Domain.Entities
{
    public class ProjectEntity
    {
        public Guid Id { get; set; }
        public string Name { get; private  set; } = string.Empty;
        public string? Description { get; set; }
        public Guid WorkspaceId { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();

        public ProjectEntity(string name, Guid workspaceId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty");
            Id = Guid.NewGuid();
            Name = name;
            WorkspaceId = workspaceId;
            CreatedAt = DateTime.UtcNow;
        }
        private ProjectEntity() { }

        public void Rename(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("Name cannot be empty");
            Name = newName;
        }
    }
}
