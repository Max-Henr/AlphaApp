using Business.Interfaces;
using Business.Models;
using Business.Services;
using Data.Context;
using Data.Entities;
using Domain.Extensions;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ProjectController(IAppUserService appUserService, IClientService clientService, IProjectService projectService, IStatusService statusService) : Controller
    {


        private readonly IAppUserService _appUserService = appUserService;
        private readonly IClientService _clientService = clientService;
        private readonly IProjectService _projectService = projectService;
        private readonly IStatusService _statusService = statusService;

        public async Task<IActionResult> Project()
        {
            var model = await BuildProjectConnectModel();
            return View("Project", model);
        }

        public async Task<IActionResult> Started()
        {
            var model = await BuildProjectConnectModel("1");
            return View("Project", model);
        }

        public async Task<IActionResult> Completed()
        {
            var model = await BuildProjectConnectModel("2");
            return View("Project", model);
        }

        // Helper Method
        private async Task<ProjectConnectModel> BuildProjectConnectModel(string? statusFilter = null)
        {
            var membersResult = await _appUserService.GetAppUserAsync();
            var clientsResult = await _clientService.GetClientAsync();
            var projectsResult = await _projectService.GetProjectsAsync();
            var statusResult = await _statusService.GetStatusAsync();

            var members = membersResult.Result?.ToList() ?? new List<AppUser>();
            var clients = clientsResult.Result?.ToList() ?? new List<Client>();
            var projects = projectsResult.Result?.ToList() ?? new List<Project>();
            var status = statusResult.Result?.ToList() ?? new List<Status>();

            var filteredProjects = projects;

            //Tagit Hjälp utav ChatGpt
            if (!string.IsNullOrEmpty(statusFilter))
            {
                filteredProjects = projects
                    .Where(p => p.Status.Id != null &&
                                p.Status.Id.Equals(statusFilter, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            //-----------------------
            var count = projects
                .Where(d => d.EndDate > DateTime.Today)
                .OrderBy(d => d.EndDate)
                .FirstOrDefault();
            if (count is not null)
            {
                ViewBag.DaysLeft = (count.EndDate - DateTime.Today).Days;
            }
            else
            {
                ViewBag.DaysLeft = 0;
            }
            return new ProjectConnectModel
            {
                TeamMembers = members,
                Form = new ProjectModel(),
                Clients = clients,
                Projects = filteredProjects,
                Statuses = status,
                SelectedStatus = statusFilter,
                AllProjects = projects
            };
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> AddProject(ProjectConnectModel model)
        {
            var addProjectFormData = model.Form.MapTo<AddProjectFormData>();
            var result = await _projectService.CreateProjectAsync(addProjectFormData);

            return RedirectToAction("Project");

        }

        [HttpPost]
        public async Task<IActionResult> DeleteProject(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Project");
            }

            var result = await _projectService.RemoveAsync(id);
            if (result.IsSuccess)
            {
                return RedirectToAction("Project");
            }
            return RedirectToAction("Project");

        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> EditProject(string id)
        {
            var projectResult = await _projectService.GetProjectAsync(id);
            var membersResult = await _appUserService.GetAppUserAsync();
            var clientsResult = await _clientService.GetClientAsync();
            var statusResult = await _statusService.GetStatusAsync();

            var member = membersResult.Result?.ToList() ?? new List<AppUser>();
            var client = clientsResult.Result?.ToList() ?? new List<Client>();
            var status = statusResult.Result?.ToList() ?? new List<Status>();

            var projectModel = new ProjectConnectModel()
            {
                TeamMembers = member,
                Form = projectResult.Result!.MapTo<ProjectModel>(),
                Clients = client,
                Projects = new List<Project>(),
                Statuses = status,
            };

            return PartialView("Partials/_EditProjectPartial", projectModel);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProject(ProjectConnectModel model)
        {
            var updateProjectFormData = model.Form.MapTo<UpdateProjectFormData>();
            var result = await _projectService.UpdateProjectAsync(updateProjectFormData);
            return RedirectToAction("Project");
        }
    }
}
