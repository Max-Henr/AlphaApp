using System.Diagnostics;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Domain.Extensions;
using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Business.Services;

public class AppUserService(IAppUserRepository appUserRepository, UserManager<AppUserEntity> userManager, RoleManager<IdentityRole> roleManager) : IAppUserService
{
    private readonly IAppUserRepository _appUserRepository = appUserRepository;
    private readonly UserManager<AppUserEntity> _userManager = userManager;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;

    public async Task<AppUserResult> GetAppUserAsync()
    {
        var result = await _appUserRepository.GetAllAsync();
        return result.MapTo<AppUserResult>();
    }
    public async Task<AppUser> GetAppUserByEmailAsync(string email)
    {
        var response = await _appUserRepository.GetAsync(x => x.Email == email);

        return new AppUser
        {
            Email = response.Result?.Email,
            FirstName = response.Result?.FirstName,
            LastName = response.Result?.LastName,
        };
    }
    public async Task<AppUserResult> AddAppUserToRole(string userId, string roleName)
    {
        if (!await _roleManager.RoleExistsAsync(roleName))
            return new AppUserResult { IsSuccess = false, StatusCode = 404, ErrorMessage = "Role does not exist" };

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return new AppUserResult { IsSuccess = false, StatusCode = 404, ErrorMessage = "User does not exist" };

        var result = await _userManager.AddToRoleAsync(user, roleName);
        return result.Succeeded
            ? new AppUserResult { IsSuccess = true, StatusCode = 200 }
            : new AppUserResult { IsSuccess = false, StatusCode = 500, ErrorMessage = "Error adding user to role" };
    }

    public async Task<AppUserResult> CreateAppUserAsync(RegisterFormData formData, string roleName = "User")
    {
        if (formData == null)
            return new AppUserResult { IsSuccess = false, StatusCode = 400, ErrorMessage = "Invalid data" };
        var existsResult = await _appUserRepository.ExistsAsync(x => x.Email == formData.Email);
        if (existsResult.IsSuccess)
            return new AppUserResult { IsSuccess = false, StatusCode = 400, ErrorMessage = "User already exists" };

        try
        {
            var appUserEntity = formData.MapTo<AppUserEntity>();
            appUserEntity.UserName = formData.Email;
            var result = await _userManager.CreateAsync(appUserEntity, formData.Password);



            if (result.Succeeded)
            {
                var addToRoleResult = await AddAppUserToRole(appUserEntity.Id, roleName);
                return addToRoleResult.IsSuccess
                    ? new AppUserResult { IsSuccess = true, StatusCode = 201 }
                    : new AppUserResult { IsSuccess = false, StatusCode = 201, ErrorMessage = "User Created But Not Added To Default Role" };
            }

            return new AppUserResult { IsSuccess = false, StatusCode = 500, ErrorMessage = "Error creating user" };

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return new AppUserResult { IsSuccess = false, StatusCode = 500, ErrorMessage = ex.Message };
        }
    }
}
