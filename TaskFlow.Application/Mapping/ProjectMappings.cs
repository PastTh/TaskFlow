using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.DTOs;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Application.Mapping
{
    public static class ProjectMappings
    {
        public static ProjectDto ToDto(this ProjectEntity project)
        {
            return new ProjectDto
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                WorkspaceId = project.WorkspaceId,
                CreatedAt = project.CreatedAt
            };
        }
    }
}
