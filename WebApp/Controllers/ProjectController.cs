using Business.Interfaces;
using Business.Services;
using Data.Context;
using Data.Entities;
using Domain.Extensions;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ProjectController(IAppUserService appUserService, IClientService clientService, IProjectService projectService) : Controller
    {


        private readonly IAppUserService _appUserService = appUserService;
        private readonly IClientService _clientService = clientService;
        private readonly IProjectService _projectService = projectService;


        public async Task<IActionResult> Project()
        {
            var members = await _appUserService.GetAppUserAsync();
            var clients = await _clientService.GetClientAsync();

            var member = members.Result?.ToList() ?? new List<AppUser>();
            var client = clients.Result?.ToList() ?? new List<Client>();

            var projectModel = new ProjectConnectModel()
            {
                TeamMembers = member,
                Form = new ProjectModel(),
                Clients = client
            };
            return View(projectModel);
        }
        [HttpPost]
        public async Task<IActionResult> Project(ProjectConnectModel model)
        {
             var addProjectFormData = model.Form.MapTo<AddProjectFormData>();
             var result = await _projectService.CreateProjectAsync(addProjectFormData);

             return RedirectToAction("Project");

        }

    }
}
