using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Domain.Extensions;
using Domain.Models;

namespace Business.Services;

public class ProjectService(IProjectRepository projectRepository, IStatusService statusService) : IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;
    private readonly IStatusService _statusService = statusService;


    public async Task<ProjectResult> CreateProjectAsync(AddProjectFormData formData)
    {

        if (formData == null)
            return new ProjectResult { IsSuccess = false, StatusCode = 400, ErrorMessage = "Invalid data" };

        if(formData.StartDate == null || formData.EndDate == null)
        {
            return new ProjectResult { IsSuccess = false, StatusCode = 400, ErrorMessage = "Start date and end date are required" };
        }


        var projectEntity = formData.MapTo<ProjectEntity>();
        var statusResult = await _statusService.GetStatusByIdAsync("1");
        var status = statusResult.Result;
        projectEntity.StatusId = status!.Id;

        projectEntity.ProjectTeamMember = formData.TeamMemberIds.Select(userId => new ProjectTeamMemberEntity
        {
            AppUserId = userId,
            ProjectId = projectEntity.Id 
        }).ToList();

        var result = await _projectRepository.CreateAsync(projectEntity);
        return result.IsSuccess
            ? new ProjectResult { IsSuccess = true, StatusCode = 201 }
            : new ProjectResult { IsSuccess = false, StatusCode = 500, ErrorMessage = result.ErrorMessage };
    }

    public async Task<ProjectResult<IEnumerable<Project>>> GetProjectsAsync()
    {
        var response = await _projectRepository.GetAllAsync
            (
                orderByDescending: true,
                sortBy: s => s.Created,
                filterBy: null,
                i => i.ProjectTeamMember,
                i => i.Status,
                i => i.Client
            );
        return response.MapTo<ProjectResult<IEnumerable<Project>>>();

    }
    public async Task<ProjectResult<Project>> GetProjectAsync(String id)
    {
        var response = await _projectRepository.GetAsync
            (
                filterBy: x => x.Id == id,
                i => i.ProjectTeamMember,
                i => i.Status,
                i => i.Client
            );

        return response.MapTo<ProjectResult<Project>>();

    }
}
