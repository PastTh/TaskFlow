using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskFlow.Application.DTOs
{
    public class CommentDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public Guid TaskItemId { get; set; }
        public string AuthorUserId { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
