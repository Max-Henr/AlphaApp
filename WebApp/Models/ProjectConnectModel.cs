using Business.Services;
using Domain.Models;

namespace WebApp.Models;

public class ProjectConnectModel
{
    public ProjectModel Form { get; set; } = new ProjectModel();

    public IEnumerable<AppUser> TeamMembers { get; set; } = [];

    public IEnumerable<Client> Clients { get; set; } = [];

    public IEnumerable<Project> Projects { get; set; } = [];
    public List<Project> AllProjects { get; set; } = [];

    public IEnumerable<Status> Statuses { get; set; } = [];
    public string? SelectedStatus { get; set; } = null;
}
