using Data.Context;
using Data.Entities;
using Data.Interfaces;
using Domain.Models;

namespace Data.Repositories;
public class AppUserRepository(AppDBContext context) : BaseRepository<AppUserEntity, AppUser>(context), IAppUserRepository
{

}
