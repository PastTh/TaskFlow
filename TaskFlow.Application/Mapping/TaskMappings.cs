using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.DTOs;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Application.Mapping
{
        public static class TaskMappings
        {
            public static TaskItemDto ToDto(this TaskItem task)
            {
                return new TaskItemDto
                {
                    Id = task.Id,
                    Title = task.Title,
                    Description = task.Description,
                    Status = task.Status.ToString(),
                    Priority = task.Priority,
                    ProjectId = task.ProjectId,
                    AssignedToUserId = task.AssignedToUserId,
                    CreatedAt = task.CreatedAt
                };
            }
        }
    }

