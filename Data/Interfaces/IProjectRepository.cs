using Data.Entities;
using Data.Models;
using Domain.Models;


namespace Data.Interfaces;

public interface IProjectRepository : IBaseRepository<ProjectEntity, Project>
{
    Task<RepositoryResult<bool>> UpdateWithMembers(ProjectEntity entity);
}
