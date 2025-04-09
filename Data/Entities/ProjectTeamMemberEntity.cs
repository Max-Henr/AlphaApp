namespace Data.Entities;

public class ProjectTeamMemberEntity
{
    public string ProjectId { get; set; } = null!;
    public virtual ProjectEntity Project { get; set; } = null!;
    public string AppUserId { get; set; } = null!;
    public virtual AppUserEntity AppUser { get; set; } = null!;
}
