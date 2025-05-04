using Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class HeaderController(IAppUserService appUserService) : Controller
{

    private readonly IAppUserService _appUserService = appUserService;

    [HttpGet]
    public async Task<IActionResult> GetHeaderData()
    {
        var email = User.Identity?.Name;
        if (string.IsNullOrEmpty(email))
            return Json(new { userName = "Guest" });

        var userResult = await _appUserService.GetAppUserByEmailAsync(email);

        var userName = userResult.FirstName + " " + userResult.LastName;
        return Json(new { userName });

    }
}
