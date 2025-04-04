﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Data.Entities;

public class ProjectEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string? Image { get; set; }
    public string ProjectName { get; set; } = null!;
    public string ClientName { get; set; } = null!;
    public string ProjectDescription { get; set; } = null!;
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public decimal Budget { get; set; }

    [ForeignKey(nameof(Client))]
    public string ClientId { get; set; } = null!;
    public virtual ClientEntity Client { get; set; } = null!;

    [ForeignKey(nameof(AppUser))]
    public string AppUserId { get; set; } = null!;
    public virtual AppUserEntity AppUser { get; set; } = null!;

    [ForeignKey(nameof(Status))]
    public string StatusId { get; set; } = null!;
    public virtual StatusEntity Status { get; set; } = null!;

}
