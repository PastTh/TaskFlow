using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.DTOs;
using TaskFlow.Web.Services;

namespace TaskFlow.Web.Controllers
{
    public class WorkspaceController : Controller
    {
        private readonly ApiClient _api;

        public WorkspaceController(ApiClient api)
        {
            _api = api;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (Request.Cookies["jwt"] == null)
                return RedirectToAction("Login", "Account");

            var workspaces = await _api.GetAsync<List<WorkspaceDto>>("api/workspaces/mine");

            return View(workspaces ?? new List<WorkspaceDto>());
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return RedirectToAction(nameof(Index));

            await _api.PostAsync("api/workspaces", new { Name = name });

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _api.DeleteAsync($"api/workspaces/{id}");

            return RedirectToAction(nameof(Index));
        }
    }
}