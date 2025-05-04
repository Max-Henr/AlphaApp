using System.ComponentModel.DataAnnotations.Schema;


namespace Domain.Models;

public class Project
{
    public string Id { get; set; } = null!;
    public string? Image { get; set; }
    public string ProjectName { get; set; } = null!;
    public string ProjectDescription { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; } 
    public decimal Budget { get; set; }
    public string ClientName { get; set; } = null!;
    public Client Client { get; set; } = null!;
    public AppUser AppUser { get; set; } = null!;
    public string StatusId { get; set; } = null!;
    public Status Status { get; set; } = null!;
    public List<string> TeamMemberNames { get; set; } = new();
    public int DaysLeft => (EndDate - DateTime.Now).Days;
}
