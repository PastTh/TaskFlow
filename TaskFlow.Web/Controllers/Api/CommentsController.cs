using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using TaskFlow.Application.DTOs;
using TaskFlow.Application.Interfaces;
using TaskFlow.Application.Mapping;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Web.Controllers.Api
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("task/{taskItemId}")]
        public async Task<IActionResult> GetByTask(Guid taskItemId)
        {
            var comments = await _unitOfWork.Comments.GetByTaskIdAsync(taskItemId);
            var dtos = comments.Select(c => c.ToDto()).ToList();
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var comment = await _unitOfWork.Comments.GetByIdAsync(id);

            if (comment == null)
                return NotFound();

            return Ok(comment.ToDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCommentDto dto)
        {
            var authorUserId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            if (authorUserId == null)
                return Unauthorized();

            var comment = new Comment(dto.Content, dto.TaskItemId, authorUserId);

            await _unitOfWork.Comments.AddAsync(comment);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = comment.Id }, comment.ToDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var comment = await _unitOfWork.Comments.GetByIdAsync(id);

            if (comment == null)
                return NotFound();

            _unitOfWork.Comments.Delete(comment);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}