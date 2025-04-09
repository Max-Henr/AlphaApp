using Domain.Models;

namespace WebApp.Models;

public class ProjectConnectModel
{
    public ProjectModel Form { get; set; } = new ProjectModel();

    public IEnumerable<AppUser> TeamMembers { get; set; } = [];

    public IEnumerable<Client> Clients { get; set; } = [];
}
