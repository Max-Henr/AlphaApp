using System.Linq.Expressions;
using System.Linq;
using Data.Context;
using Data.Entities;
using Data.Interfaces;
using Data.Models;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;


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
            Client = new Client
            {
                Id = entity.Client.Id,
                ClientName = entity.Client.ClientName,
            },
            Status = new Status
            {
                Id = entity.Status.Id,
                StatusName = entity.Status.StatusName,
            },
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            Budget = entity.Budget,
            TeamMemberNames = entity.ProjectTeamMember
                .Select(pt => pt.AppUser.FirstName).ToList(),
            ClientName = entity.Client.ClientName,
        }).ToList();
        return new RepositoryResult<IEnumerable<Project>>
        {
            IsSuccess = true,
            StatusCode = 200,
            Result = result
        };
    }

    //Tagit hjälp av chatgpt för detta
    public async Task<RepositoryResult<bool>> UpdateWithMembers(ProjectEntity entity)
    {
        try
        {
            var project = await _dbSet
                .Include(p => p.ProjectTeamMember)
                .FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (project is not null)
            {
                _context.Entry(project).CurrentValues.SetValues(entity);
                // Remove existing team members
                var existingTeamMembers = project.ProjectTeamMember.ToList();
                foreach (var teamMember in existingTeamMembers)
                {
                    _context.Entry(teamMember).State = EntityState.Deleted;
                }
                // Add new team members
                foreach (var teamMember in entity.ProjectTeamMember)
                {
                    _context.Entry(teamMember).State = EntityState.Added;
                }
                await _context.SaveChangesAsync();
                return new RepositoryResult<bool> { IsSuccess = true, StatusCode = 200 };
            }
            else
            {
                return new RepositoryResult<bool> { IsSuccess = false, StatusCode = 404, ErrorMessage = "Project not found" };


            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error Updating {nameof(ProjectEntity)} entity :: {ex.Message}");
            return new RepositoryResult<bool> { IsSuccess = false, StatusCode = 500, ErrorMessage = ex.Message };
        }
    }
}
