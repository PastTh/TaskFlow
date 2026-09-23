using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Application.Interfaces
{
    public interface IWorkspaceRepository
    {
        Task<Workspace?> GetByIdAsync(Guid id);
        Task<List<Workspace>> GetByUserIdAsync(string userId);
        Task AddAsync(Workspace workspace);
        void Update(Workspace workspace);
        void Delete(Workspace workspace);
    }
}
