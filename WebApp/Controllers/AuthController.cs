using Business.Interfaces;
using Business.Models;
using Business.Services;
using Data.Entities;
using Domain.Extensions;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class AuthController(SignInManager<AppUserEntity> signInManager, IAuthService authService) : Controller
    {
        private readonly IAuthService _authService = authService;
        private readonly SignInManager<AppUserEntity> _signInManager = signInManager;



        // SignIn

        public IActionResult SignIn(string returnUrl = "~/")
        {

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInModel model, string returnUrl = "~/")
        {
            ViewBag.ErrorMessage = null;
            ViewBag.ReturnUrl = returnUrl;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var signInFormData = model.MapTo<SignInFormData>();           

            var result = await _authService.SignInAsync(signInFormData);
            if (result.IsSuccess)
            {
                return LocalRedirect(returnUrl);
            }

            ViewBag.ErrorMessage = result.ErrorMessage;
            return View(model);
        }

        //Register

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            ViewBag.ErrorMessage = null;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var registerFormData = model.MapTo<RegisterFormData>();

            var result = await _authService.SignUpAsync(registerFormData);
            if (result.IsSuccess)
            {
                return RedirectToAction("SignIn", "Auth");
            }

            ViewBag.ErrorMessage = result.ErrorMessage;
            return View(model);
        }
        

        //SignOut
        public new async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
