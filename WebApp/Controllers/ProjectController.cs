﻿using Business.Interfaces;
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
            var membersResult = await _appUserService.GetAppUserAsync();
            var clientsResult = await _clientService.GetClientAsync();
            var projectsResult = await _projectService.GetProjectsAsync();

            var member = membersResult.Result?.ToList() ?? new List<AppUser>();
            var client = clientsResult.Result?.ToList() ?? new List<Client>();
            var project = projectsResult.Result?.ToList() ?? new List<Project>();


            var projectModel = new ProjectConnectModel()
            {
                TeamMembers = member,
                Form = new ProjectModel(),
                Clients = client,
                Projects = project
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

            //var projectResult = await _projectService.GetProjectAsync(id);
            //if (projectResult.IsSuccess)
            //{
            //    var result = await _projectService.DeleteProjectAsync(id);
            //    if (result.IsSuccess)
            //    {
            //        return RedirectToAction("Project");
            //    }
            //}

        }
    }
}
