namespace Data.Entities;

public class ClientEntity
{
    public string Id { get; set; } = null!;

    public string ClientName { get; set; } = null!;

    public virtual ICollection<ProjectEntity> Projects { get; set; } = [];
}
