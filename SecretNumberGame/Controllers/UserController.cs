using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using SecretNumberGame.Data.Constants;
using SecretNumberGame.Data.DataModels;
using SecretNumberGame.Services.Contracts;
using SecretNumberGame.Services.Models;

namespace SecretNumberGame.Controllers
{    
    public class UserController : BaseController
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly IUserAccountService service;
        private readonly INotyfService toastMsgService;

        public UserController(UserManager<AppUser> _userManager,
                            SignInManager<AppUser> _signInManager,                            
                            IUserAccountService service,
                            INotyfService toastMsgService)
                           

        {
            userManager = _userManager;
            signInManager = _signInManager;
            this.service = service;
            this.toastMsgService = toastMsgService;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home");
            }
            var model = new RegisterViewModel();
            return this.View(model);

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        { 
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }
            var user = new AppUser()
            {
                Email = model.Email,
                UserName = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
            };
            bool emailAlreadyExists = await userManager.Users.AnyAsync(x => x.Email == user.Email);
            if (emailAlreadyExists)
            {
                //Can be add to the ModelState Errors
                //this.ModelState.AddModelError("", GlobalConstants.EMAIL_ALREADY_EXISTS);
                toastMsgService.Warning("Email Address already exists!");
                return this.View(model);
            }
            var returnedResult = await service.GetRegisteredAsync(model,user);
            if (returnedResult.Succeeded) 
            {
                toastMsgService.Success($"Congratulations, {user.FirstName}! You have been registered successfully.");
                return RedirectToAction("Index", "Home");
            }            
            foreach (var err in returnedResult.Errors)
            {
                ModelState.AddModelError("", err.Description);
            }
            return this.View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl = null)
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home");
            }
            var model = new LoginViewModel();
            model.ReturnUrl = returnUrl;    
            return this.View(model);

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }
            bool isLoggedIn = await service.GetLoggedInAsync(model);
            if (isLoggedIn)
            {
                toastMsgService.Success($"Welcome, {model.Email}! Have a nice game time!");
                return RedirectToAction("Index", "Home");
            }            
            ModelState.AddModelError("", "Invalid login");
            return this.View(model);

        }
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home"); 
        }
    }
}
