using Microsoft.EntityFrameworkCore;
using TaskFlow.Application.Interfaces;
using TaskFlow.Domain.Entities;
using TaskFlow.Infrastructure.Persistence;

namespace TaskFlow.Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
       private readonly TaskFlowDbContext _dbContext;
       
        public ProjectRepository(TaskFlowDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<ProjectEntity?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Projects.FindAsync(id);
        }

        public async Task<List<ProjectEntity>> GetByWorkspaceIdAsync(Guid workspaceId)
        {
            return await _dbContext.Projects
                .Where(p => p.WorkspaceId == workspaceId)
                .ToListAsync();
        }

        public async Task AddAsync(ProjectEntity project)
        {
            await _dbContext.Projects.AddAsync(project);
        }

        public void Update(ProjectEntity project)
        {
            _dbContext.Projects.Update(project);
        }

        public void Delete(ProjectEntity project)
        {
            _dbContext.Projects.Remove(project);
        }
    }
}
