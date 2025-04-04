using Data.Context;
using Data.Entities;
using Data.Interfaces;
using Domain.Models;


namespace Data.Repositories;
public class ProjectRepository(AppDBContext context) : BaseRepository<ProjectEntity, Project>(context), IProjectRepository
{

}
