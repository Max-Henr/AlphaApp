using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Context;

public class AppDBContext(DbContextOptions<AppDBContext> options) : IdentityDbContext<AppUserEntity>(options)
{
    public DbSet<ProjectEntity> Projects { get; set; } = null!;

    public DbSet<ClientEntity> Clients { get; set; } = null!;

    public DbSet<StatusEntity> Status { get; set; } = null!;

    public DbSet<ProjectTeamMemberEntity> ProjectTeamMembers { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ProjectEntity>()
            .HasOne(p => p.Client)
            .WithMany(c => c.Projects)
            .HasForeignKey(p => p.ClientId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<StatusEntity>()
            .HasData
            (
            new StatusEntity
            {
                Id = "1",
                StatusName = "In Progress"
            },
            new StatusEntity
            {
                    Id = "2",
                    StatusName = "Completed"
            }
            );

        builder.Entity<IdentityRole>()
            .HasData
            (
            new IdentityRole
            {
                Id = "1",
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new IdentityRole
            {
                Id = "2",
                Name = "User",
                NormalizedName = "USER"
            }
            );
        builder.Entity<ProjectEntity>()
            .HasOne(p => p.Status)
            .WithMany(s => s.Projects)
            .HasForeignKey(p => p.StatusId);


        builder.Entity<ProjectTeamMemberEntity>()
            .HasKey(pt => new { pt.ProjectId, pt.AppUserId });

        builder.Entity<ProjectTeamMemberEntity>()
            .HasOne(pt => pt.Project)
            .WithMany(p => p.ProjectTeamMember)
            .HasForeignKey(pt => pt.ProjectId);
        builder.Entity<ProjectTeamMemberEntity>()
            .HasOne(pt => pt.AppUser)
            .WithMany(p => p.ProjectTeamMember)
            .HasForeignKey(pt => pt.AppUserId);

    }
}
