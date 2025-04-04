using Data.Entities;
using Domain.Models;

namespace Data.Interfaces;

public interface IAppUserRepository : IBaseRepository<AppUserEntity, AppUser>
{
}
