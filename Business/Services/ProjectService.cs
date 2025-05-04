using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Data.Models;
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

        if (formData.StartDate == null || formData.EndDate == null)
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
    public async Task<ProjectResult> UpdateProjectAsync(UpdateProjectFormData formData)
    {
        if (formData == null)
            return new ProjectResult { IsSuccess = false, StatusCode = 400, ErrorMessage = "Invalid data" };

        var projectEntity = new ProjectEntity
        {
            Id = formData.Id,
            Image = formData.Image,
            ProjectName = formData.ProjectName,
            ProjectDescription = formData.ProjectDescription!,
            StartDate = formData.StartDate,
            EndDate = formData.EndDate,
            Budget = formData.Budget,
            ClientId = formData.ClientId,
            StatusId = formData.StatusId,
            ProjectTeamMember = formData.TeamMemberIds.Select(userId => new ProjectTeamMemberEntity
            {
                AppUserId = userId,
                ProjectId = formData.Id
            }).ToList()
        };
        var result = await _projectRepository.UpdateWithMembers(projectEntity);
        return result.IsSuccess
            ? new ProjectResult { IsSuccess = true, StatusCode = 200 }
            : new ProjectResult { IsSuccess = false, StatusCode = 500, ErrorMessage = result.ErrorMessage };
    }
    public async Task<ProjectResult> RemoveAsync(string id)
    {
        var result = await _projectRepository.RemoveAsync(x => x.Id == id);

        return result.IsSuccess
            ? new ProjectResult { IsSuccess = true, StatusCode = 200 }
            : new ProjectResult { IsSuccess = false, StatusCode = 500, ErrorMessage = result.ErrorMessage };
    }
}

