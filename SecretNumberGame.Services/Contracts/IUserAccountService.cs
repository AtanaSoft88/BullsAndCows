using Microsoft.AspNetCore.Identity;
using SecretNumberGame.Data.DataModels;
using SecretNumberGame.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretNumberGame.Services.Contracts
{
    public interface IUserAccountService
    {
        Task<IdentityResult> GetRegisteredAsync(RegisterViewModel model, AppUser user);
        Task<bool> GetLoggedInAsync(LoginViewModel model);       
    }
}
