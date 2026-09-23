using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Domain.Enums;

namespace TaskFlow.Domain.Entities
{
    public class Workspace
    {
        public Guid Id { get; set; }
        public string Name { get; private set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<ProjectEntity> Projects { get; set; } = new List<ProjectEntity>();
        public ICollection<WorkspaceMember> Members { get; set; } = new List<WorkspaceMember>();

        private Workspace() { }
        public Workspace(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty");
            Id = Guid.NewGuid();
            Name = name;
            CreatedAt = DateTime.UtcNow;
        }

        public void Rename(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("Name cannot be empty");
            Name = newName;
        }


    }
}
