using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Application.Interfaces
{
    public interface IProjectRepository
    {
        Task<ProjectEntity?> GetByIdAsync(Guid id);
        Task<List<ProjectEntity>> GetByWorkspaceIdAsync(Guid workspaceId);
        Task AddAsync(ProjectEntity project);
        void Update(ProjectEntity project);
        void Delete(ProjectEntity project);
    }
}
