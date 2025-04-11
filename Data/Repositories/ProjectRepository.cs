using System.Linq.Expressions;
using System.Linq;
using Data.Context;
using Data.Entities;
using Data.Interfaces;
using Data.Models;
using Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace Data.Repositories;
public class ProjectRepository(AppDBContext context) : BaseRepository<ProjectEntity, Project>(context), IProjectRepository
{

    public override async Task<RepositoryResult<IEnumerable<Project>>> GetAllAsync(
        bool orderByDescending = false,
        Expression<Func<ProjectEntity, object>>? sortBy = null,
        Expression<Func<ProjectEntity, bool>>? filterBy = null,
        params Expression<Func<ProjectEntity, object>>[] includes)
    {
        IQueryable<ProjectEntity> query = _dbSet;

        if (filterBy != null)
            query = query.Where(filterBy);

        if (includes != null && includes.Length > 0)
            foreach (var include in includes)
                query = query.Include(include);

        if (sortBy != null)
            query = orderByDescending ? query.OrderByDescending(sortBy) : query.OrderBy(sortBy);

        var entities = await query.ToListAsync();

        var result = entities.Select(entity => new Project
        {
            Id = entity.Id,
            ProjectName = entity.ProjectName,
            ProjectDescription = entity.ProjectDescription,
            ClientName = entity.Client.ClientName,
            EndDate = entity.EndDate,
            TeamMemberNames = entity.ProjectTeamMember
                .Select(pt => pt.AppUser.FirstName).ToList()
        }).ToList();
        return new RepositoryResult<IEnumerable<Project>>
        {
            IsSuccess = true,
            StatusCode = 200,
            Result = result
        };
    }
}
