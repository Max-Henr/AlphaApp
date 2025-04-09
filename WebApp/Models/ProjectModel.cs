﻿using System.ComponentModel.DataAnnotations;
using Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models;

public class ProjectModel
{
    [Display(Name = "Project Name", Prompt = "Project Name")]
    [Required(ErrorMessage = "Project Name is required")]
    public string ProjectName { get; set; } = null!;

    [Display(Name = "Client Name", Prompt = "Client Name")]
    [Required(ErrorMessage = "Client Name is required")]
    public string Client { get; set; } = null!;

    [Display(Name = "Project Description", Prompt = "Project Description")]
    [Required(ErrorMessage = "Project Description is required")]
    public string ProjectDescription { get; set; } = null!;

    [Display(Name = "Start Date", Prompt = "Start Date")]
    [Required(ErrorMessage = "Start Date is required")]
    public DateOnly? StartDate { get; set; } 

    [Display(Name = "End Date", Prompt = "End Date")]
    [Required(ErrorMessage = "End Date is required")]
    public DateOnly? EndDate { get; set; }

    [Display(Name = "Team Members", Prompt = "Team Members")]
    [Required(ErrorMessage = "Team Members are required")]
    [RegularExpression(@"^[a-zA-Z, ]+$", ErrorMessage = "Invalid team members format")]
    public string TeamMembers { get; set; } = null!;

    [Display(Name = "Budget", Prompt = "Budget")]
    [Required(ErrorMessage = "Budget is required")]
    [Range(0, double.MaxValue, ErrorMessage = "Budget must be a positive number")]
    [DataType(DataType.Currency)]
    [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
    [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Invalid budget format")]
    public decimal? Budget { get; set; } 


}
