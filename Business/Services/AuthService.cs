using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Business.Services;

public class AuthService(SignInManager<AppUserEntity> signInManager, IAppUserService appUserService) : IAuthService
{
    public readonly IAppUserService _appUserService = appUserService;
    public readonly SignInManager<AppUserEntity> _signInManager = signInManager;

    public async Task<AuthResult> SignInAsync(SignInFormData formData)
    {
        if (formData == null)
            return new AuthResult { IsSuccess = false, StatusCode = 400, ErrorMessage = "Invalid data" };

        var result = await _signInManager.PasswordSignInAsync(
            formData.Email,
            formData.Password,
            formData.IsPersistent,
            lockoutOnFailure: false
        );
        return result.Succeeded
            ? new AuthResult { IsSuccess = true, StatusCode = 200 }
            : new AuthResult { IsSuccess = false, StatusCode = 401, ErrorMessage = "Invalid login" };
    }

    public async Task<AuthResult> SignUpAsync(RegisterFormData formData)
    {
        if (formData == null)
            return new AuthResult { IsSuccess = false, StatusCode = 400, ErrorMessage = "Invalid data" };

        var result = await _appUserService.CreateAppUserAsync(formData);
        return result.IsSuccess
            ? new AuthResult { IsSuccess = true, StatusCode = 201 }
            : new AuthResult { IsSuccess = false, StatusCode = 500, ErrorMessage = result.ErrorMessage };

    }

    public async Task<AuthResult> SignOutAsync()
    {
        await _signInManager.SignOutAsync();
        return new AuthResult { IsSuccess = true, StatusCode = 200 };
    }
}