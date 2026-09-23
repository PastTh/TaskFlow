using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskFlow.Application.Interfaces
{
    public interface IUnitOfWork
    {
        ITaskRepository Tasks { get; }
        IProjectRepository Projects { get; }
        IWorkspaceRepository Workspaces { get; }
        ICommentRepository Comments { get; }

        Task<int> SaveChangesAsync();
    }
}
