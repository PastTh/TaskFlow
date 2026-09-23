using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.DTOs;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Application.Mapping
{
    public static class CommentMappings
    {
        public static CommentDto ToDto(this Comment comment)
        {
            return new CommentDto
            {
                Id = comment.Id,
                Content = comment.Content,
                TaskItemId = comment.TaskItemId,
                AuthorUserId = comment.AuthorUserId,
                CreatedAt = comment.CreatedAt
            };
        }
    }
}
