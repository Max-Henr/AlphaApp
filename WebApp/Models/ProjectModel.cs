using System.ComponentModel.DataAnnotations;
using Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models;

public class ProjectModel
{
    public string Id { get; set; } = null!;
    [Display(Name = "Project Name", Prompt = "Project Name")]
    [Required(ErrorMessage = "Project Name is required")]
    public string ProjectName { get; set; } = null!;

    [Display(Name = "Client Name", Prompt = "Client Name")]
    [Required(ErrorMessage = "Client Name is required")]
    public string ClientId { get; set; } = null!;
    public string ClientName { get; set; } = null!;

    [Display(Name = "Project Description", Prompt = "Project Description")]
    [Required(ErrorMessage = "Project Description is required")]
    public string ProjectDescription { get; set; } = null!;

    [Display(Name = "Start Date", Prompt = "Start Date")]
    [Required(ErrorMessage = "Start Date is required")]
    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; } = DateTime.Today;

    [Display(Name = "End Date", Prompt = "End Date")]
    [Required(ErrorMessage = "End Date is required")]
    [DataType(DataType.Date)]
    public DateTime EndDate { get; set; } = DateTime.Today.AddDays(7);

    [Display(Name = "Team Members", Prompt = "Team Members")]
    [Required(ErrorMessage = "Team Members are required")]
    public List<string> TeamMemberIds { get; set; } = [];

    [Display(Name = "Budget", Prompt = "Budget")]
    [Required(ErrorMessage = "Budget is required")]
    [Range(0, double.MaxValue, ErrorMessage = "Budget must be a positive number")]
    [DataType(DataType.Currency)]
    [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
    [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Invalid budget format")]
    public decimal Budget { get; set; }
    [Display(Name = "Project Status", Prompt = "Project Status")]
    public string? StatusId { get; set; } 
    public string? StatusName { get; set; } 


}
