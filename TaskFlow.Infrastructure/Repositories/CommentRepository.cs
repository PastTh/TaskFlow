using Microsoft.EntityFrameworkCore;
using TaskFlow.Application.Interfaces;
using TaskFlow.Domain.Entities;
using TaskFlow.Infrastructure.Persistence;

namespace TaskFlow.Infrastructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly TaskFlowDbContext _context;
        public CommentRepository(TaskFlowDbContext context)
        {
            _context = context;
        }
        public async Task<Comment?> GetByIdAsync(Guid id)
        {
            return await _context.Comments.FindAsync(id);
        }
        public async Task<List<Comment>> GetByTaskIdAsync(Guid taskItemId)
        {
            return await _context.Comments
                .Where(c => c.TaskItemId == taskItemId)
                .ToListAsync();
        }
        public async Task AddAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
        }
        public void Update(Comment comment)
        {
            _context.Comments.Update(comment);            
        }
        public void Delete(Comment comment)
        {
            _context.Comments.Remove(comment);
        }
    }
}
