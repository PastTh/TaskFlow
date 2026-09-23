using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.DTOs;
using TaskFlow.Application.Interfaces;
using TaskFlow.Application.Mapping;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Web.Controllers.Api
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<TasksController> _logger;

        public TasksController(IUnitOfWork unitOfWork, ILogger<TasksController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet("project/{projectId}")]
        public async Task<IActionResult> GetByProject(Guid projectId, int page = 1, int pageSize = 10)
        {
            var tasks = await _unitOfWork.Tasks.GetByProjectIdAsync(projectId, page, pageSize);
            var dtos = tasks.Select(t => t.ToDto()).ToList();
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var task = await _unitOfWork.Tasks.GetByIdAsync(id);

            if (task == null)
                return NotFound();

            return Ok(task.ToDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTaskItemDto dto)
        {
            var task = new TaskItem(dto.Title, dto.ProjectId);

            await _unitOfWork.Tasks.AddAsync(task);
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation("Task {TaskId} created in project {ProjectId}", task.Id, task.ProjectId);

            return CreatedAtAction(nameof(GetById), new { id = task.Id }, task.ToDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var task = await _unitOfWork.Tasks.GetByIdAsync(id);

            if (task == null)
                return NotFound();

            _unitOfWork.Tasks.Delete(task);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}