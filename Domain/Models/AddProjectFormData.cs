namespace Domain.Models;

public class AddProjectFormData
{
    public string ProjectName { get; set; } = null!;
    public string? Image { get; set; }
    public string? ProjectDescription { get; set; }
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public decimal Budget { get; set; } 
    public string ClientId { get; set; } = null!;
    public List<string> TeamMemberIds { get; set; } = [];
    public int StatusId { get; set; }
}
