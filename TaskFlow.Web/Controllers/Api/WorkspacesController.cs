using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using TaskFlow.Application.DTOs;
using TaskFlow.Application.Interfaces;
using TaskFlow.Application.Mapping;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Enums;

namespace TaskFlow.Web.Controllers.Api
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class WorkspacesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public WorkspacesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("mine")]
        public async Task<IActionResult> GetMine()
        {
            var userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            if (userId == null)
                return Unauthorized();

            var workspaces = await _unitOfWork.Workspaces.GetByUserIdAsync(userId);
            var dtos = workspaces.Select(w => w.ToDto()).ToList();
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var workspace = await _unitOfWork.Workspaces.GetByIdAsync(id);

            if (workspace == null)
                return NotFound();

            return Ok(workspace.ToDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateWorkspaceDto dto)
        {
            var userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            if (userId == null)
                return Unauthorized();

            var workspace = new Workspace(dto.Name);
            var member = new WorkspaceMember(workspace.Id, userId, MemberRole.Owner);

            workspace.Members.Add(member);

            await _unitOfWork.Workspaces.AddAsync(workspace);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = workspace.Id }, workspace.ToDto());
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var workspace = await _unitOfWork.Workspaces.GetByIdAsync(id);

            if (workspace == null)
                return NotFound();

            _unitOfWork.Workspaces.Delete(workspace);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}