using TaskFlow.Application.Interfaces;
using TaskFlow.Infrastructure.Persistence;

namespace TaskFlow.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TaskFlowDbContext _context;

        public ITaskRepository Tasks { get; }
        public IProjectRepository Projects { get; }
        public IWorkspaceRepository Workspaces { get; }
        public ICommentRepository Comments { get; }

        public UnitOfWork(
            TaskFlowDbContext context,
            ITaskRepository tasks,
            IProjectRepository projects,
            IWorkspaceRepository workspaces,
            ICommentRepository comments)
        {
            _context = context;
            Tasks = tasks;
            Projects = projects;
            Workspaces = workspaces;
            Comments = comments;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}