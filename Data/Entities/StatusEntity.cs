namespace Data.Entities;

public class StatusEntity
{
    public string Id { get; set; } = null!;
    public string StatusName { get; set; } = null!;
    public virtual ICollection<ProjectEntity> Projects { get; set; } = [];
}
