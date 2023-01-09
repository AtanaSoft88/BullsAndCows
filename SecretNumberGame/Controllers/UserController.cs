using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using SecretNumberGame.Data.DataModels;
using SecretNumberGame.Services.Models;

namespace SecretNumberGame.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        

        public UserController(UserManager<AppUser> _userManager,
                            SignInManager<AppUser> _signInManager)
                           

        {
            this.userManager = _userManager;
            this.signInManager = _signInManager;
            
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            if (User?.Identity?.IsAuthenticated??false)
            {
                return RedirectToAction("All", "Books");
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
            var alreadyExists = await userManager.Users.AnyAsync(x => x.Email == user.Email);
            if (alreadyExists)
            {
                this.ModelState.AddModelError("", "There is already registered such user email! Please consider getting new one for register.");
                return this.View(model);
            }

            var result = await userManager.CreateAsync(user, model.Password);
            
            if (result.Succeeded)
            {
                await userManager.AddClaimAsync(user, new System.Security.Claims.Claim("first_name", user.FirstName ?? user.Email));
                await signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Login", "User");
                
            }

            
            foreach (var err in result.Errors)
            {
                ModelState.AddModelError("", err.Description);
            }
            return this.View(model);
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home");
            }
            var model = new LoginViewModel();
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

            var user = await userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {                    
                var result = await signInManager.PasswordSignInAsync(user, model.Password, isPersistent: true, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

            }
            ModelState.AddModelError("", "Invalid login");
            return this.View(model);

        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home"); 
        }
    }
}
