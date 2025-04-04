using Business.Interfaces;
using Business.Services;
using Domain.Extensions;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ProjectController(IProjectService projectService, IAppUserService appUserService) : Controller
    {

        private readonly IProjectService _projectService = projectService;
        private readonly IAppUserService _appUserService = appUserService;

        public IActionResult Project()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Add(ProjectModel model)
        {

            var addProjectFormData = model.MapTo<AddProjectFormData>();

            var result = _projectService.CreateProjectAsync(addProjectFormData);

            return Json(new { });
        }

    }
}
