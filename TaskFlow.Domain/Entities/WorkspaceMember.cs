using TaskFlow.Domain.Enums;

namespace TaskFlow.Domain.Entities
{
    public class WorkspaceMember
    {
        public Guid Id { get; set; }
        public Guid WorkspaceId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public MemberRole Role { get; private set; }

        private WorkspaceMember() { }

        public WorkspaceMember(Guid workspaceId, string userId, MemberRole role)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException("UserId cannot be empty");

            Id = Guid.NewGuid();
            WorkspaceId = workspaceId;
            UserId = userId;
            Role = role;
        }

        public void ChangeRole(MemberRole newRole)
        {
            Role = newRole;
        }
    }
}