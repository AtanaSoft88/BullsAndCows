using Microsoft.AspNetCore.Identity;
using SecretNumberGame.Data.DataModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecretNumberGame.Services.Contracts;
using SecretNumberGame.Data.Constants;
using SecretNumberGame.Services.Models;

namespace SecretNumberGame.Services
{
    public class UserAccountService : IUserAccountService
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly RoleManager<IdentityRole<Guid>> roleManager;

        public UserAccountService(UserManager<AppUser> _userManager,
                            SignInManager<AppUser> _signInManager,
                            RoleManager<IdentityRole<Guid>> _roleManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            roleManager = _roleManager;
        }

        public async Task<bool> GetLoggedInAsync(LoginViewModel model)
        {
            AppUser user = await userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var result = await signInManager.PasswordSignInAsync(user, model.Password, isPersistent: true, false);
                if (result.Succeeded)
                {
                    return true;
                } 
            }
            return false;
        }

        public async Task<IdentityResult> GetRegisteredAsync(RegisterViewModel model, AppUser user)
        {                  
            IdentityResult result = await userManager.CreateAsync(user, model.Password);           

            if (result.Succeeded)
            {
                await userManager.AddClaimAsync(user, new System.Security.Claims.Claim("first_name", user.FirstName ?? user.Email));

                if (await roleManager.Roles.AnyAsync() == false)
                {
                    await roleManager.CreateAsync(new IdentityRole<Guid>(GlobalConstants.ADMIN));
                    await roleManager.CreateAsync(new IdentityRole<Guid>(GlobalConstants.PLAYER));
                }
                
                if (userManager.Users.Count() == 1)
                {
                    await userManager.AddToRolesAsync(user, new string[]
                    {   GlobalConstants.ADMIN,
                        GlobalConstants.PLAYER
                    });
                }
                else
                {
                    await userManager.AddToRoleAsync(user, GlobalConstants.PLAYER);
                }
                
            }
            return result;
        }                
    }
}
