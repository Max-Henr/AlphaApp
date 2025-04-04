using Data.Context;
using Data.Entities;
using Data.Interfaces;
using Domain.Models;

namespace Data.Repositories;
public class StatusRepository(AppDBContext context) : BaseRepository<StatusEntity, Status>(context), IStatusRepository
{
}
