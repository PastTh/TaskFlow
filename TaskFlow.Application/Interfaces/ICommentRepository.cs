using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Application.Interfaces
{
    public interface ICommentRepository
    {
        Task<Comment?> GetByIdAsync(Guid id);
        Task<List<Comment>> GetByTaskIdAsync(Guid taskItemId);
        Task AddAsync(Comment comment);
        void Update(Comment comment);
        void Delete(Comment comment);
    }
}
