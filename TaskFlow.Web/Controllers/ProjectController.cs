using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.DTOs;
using TaskFlow.Web.Services;

namespace TaskFlow.Web.Controllers
{
    public class ProjectController : Controller
    {
        private readonly ApiClient _api;

        public ProjectController(ApiClient api)
        {
            _api = api;
        }

        [HttpGet]
        public async Task<IActionResult> Index(Guid workspaceId)
        {
            if (Request.Cookies["jwt"] == null)
                return RedirectToAction("Login", "Account");

            var projects = await _api.GetAsync<List<ProjectDto>>($"api/projects/workspace/{workspaceId}");

            ViewBag.WorkspaceId = workspaceId;
            return View(projects ?? new List<ProjectDto>());
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name, Guid workspaceId)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                await _api.PostAsync("api/projects", new { Name = name, WorkspaceId = workspaceId });
            }

            return RedirectToAction(nameof(Index), new { workspaceId });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id, Guid workspaceId)
        {
            await _api.DeleteAsync($"api/projects/{id}");

            return RedirectToAction(nameof(Index), new { workspaceId });
        }
    }
}