using Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Context;

public class AppDBContext(DbContextOptions<AppDBContext> options) : IdentityDbContext<AppUserEntity>(options)
{
    public DbSet<ProjectEntity> Projects { get; set; } = null!;

    public DbSet<ClientEntity> Notes { get; set; } = null!;

    public DbSet<StatusEntity> Clients { get; set; } = null!;
}
