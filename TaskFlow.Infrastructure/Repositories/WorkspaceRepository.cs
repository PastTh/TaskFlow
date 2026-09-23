using Microsoft.EntityFrameworkCore;
using TaskFlow.Application.Interfaces;
using TaskFlow.Domain.Entities;
using TaskFlow.Infrastructure.Persistence;

namespace TaskFlow.Infrastructure.Repositories
{
    public class WorkspaceRepository : IWorkspaceRepository
    {
        private readonly TaskFlowDbContext _context;

        public WorkspaceRepository(TaskFlowDbContext context)
        {
            _context = context;
        }

        public async Task<Workspace?> GetByIdAsync(Guid id)
        {
            return await _context.Workspaces.FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<List<Workspace>> GetByUserIdAsync(string userId)
        {
            var workspaceIds = await _context.WorkspaceMembers
                .Where(m => m.UserId == userId)
                .Select(m => m.WorkspaceId)
                .ToListAsync();

            return await _context.Workspaces
                .Where(w => workspaceIds.Contains(w.Id))
                .ToListAsync();
        }

        public async Task AddAsync(Workspace workspace)
        {
            await _context.Workspaces.AddAsync(workspace);
        }

        public void Update(Workspace workspace)
        {
            _context.Workspaces.Update(workspace);
        }

        public void Delete(Workspace workspace)
        {
            _context.Workspaces.Remove(workspace);
        }
    }
}