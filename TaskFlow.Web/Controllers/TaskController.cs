using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.DTOs;
using TaskFlow.Web.Services;

namespace TaskFlow.Web.Controllers
{
    public class TaskController : Controller
    {
        private readonly ApiClient _api;

        public TaskController(ApiClient api)
        {
            _api = api;
        }

        [HttpGet]
        public async Task<IActionResult> Index(Guid projectId)
        {
            if (Request.Cookies["jwt"] == null)
                return RedirectToAction("Login", "Account");

            var tasks = await _api.GetAsync<List<TaskItemDto>>($"api/tasks/project/{projectId}");

            ViewBag.ProjectId = projectId;
            return View(tasks ?? new List<TaskItemDto>());
        }

        [HttpPost]
        public async Task<IActionResult> Create(string title, Guid projectId)
        {
            if (!string.IsNullOrWhiteSpace(title))
            {
                await _api.PostAsync("api/tasks", new { Title = title, ProjectId = projectId, Priority = 0 });
            }

            return RedirectToAction(nameof(Index), new { projectId });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id, Guid projectId)
        {
            await _api.DeleteAsync($"api/tasks/{id}");

            return RedirectToAction(nameof(Index), new { projectId });
        }
    }
}