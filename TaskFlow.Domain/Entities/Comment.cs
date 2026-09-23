using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TaskFlow.Domain.Entities
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Content { get; private set; } = string.Empty;
        public Guid TaskItemId { get; set; }
        public string AuthorUserId { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        private Comment() { }
        public Comment(string content, Guid taskItemId, string authorUserId)
        {
            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentException("Content cannot be empty");
            Id = Guid.NewGuid();
            Content = content;
            TaskItemId = taskItemId;
            AuthorUserId = authorUserId;
            CreatedAt = DateTime.UtcNow;
        }
        public void Edit(string newContent)
        {
            if (string.IsNullOrWhiteSpace(newContent))
                throw new ArgumentException("Content cannot be empty");
            Content = newContent;
        }
    }
}