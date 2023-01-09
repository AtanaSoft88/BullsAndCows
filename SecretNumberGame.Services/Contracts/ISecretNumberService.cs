using SecretNumberGame.Data.DataModels;
using SecretNumberGame.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretNumberGame.Services.Contracts
{
    public interface ISecretNumberService
    {
        Task<SecretNumberViewModel> GetSecretNumberAsync();
        Task<SecretNumberViewModel> GetModelAsJson(string nums, int num, int col);        
    }
}
