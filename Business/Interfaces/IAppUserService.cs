using Business.Models;
using Domain.Models;

namespace Business.Services
{
    public interface IAppUserService
    {
        Task<AppUserResult> AddAppUserToRole(string userId, string roleName);
        Task<AppUserResult> CreateAppUserAsync(RegisterFormData formData, string roleName = "User");
        Task<AppUserResult> GetAppUserAsync();
    }
}