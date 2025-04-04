using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public class Project
{
    public string Id { get; set; } = null!;
    public string? Image { get; set; }
    public string ProjectName { get; set; } = null!;
    public string ClientName { get; set; } = null!;
    public string ProjectDescription { get; set; } = null!;
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public decimal Budget { get; set; }
    public Client Client { get; set; } = null!;
    public AppUser AppUser { get; set; } = null!;
    public Status Status { get; set; } = null!;
}
