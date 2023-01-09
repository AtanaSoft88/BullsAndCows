using SecretNumberGame.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretNumberGame.Services.Contracts
{
    public interface ISaveResultService
    {
        Task<ResultScoreViewModel> AddResultToDb(ResultScoreViewModel model);
        Task<ResultScoreViewModel> CollectModelProperties(string secretNum, DateTime startDt, string bombs, string userId);
    }
}
