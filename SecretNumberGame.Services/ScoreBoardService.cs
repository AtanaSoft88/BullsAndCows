using Microsoft.EntityFrameworkCore;
using SecretNumberGame.Data;
using SecretNumberGame.Services.Contracts;
using SecretNumberGame.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretNumberGame.Services
{
    public class ScoreBoardService : IScoreBoardService
    {
        private readonly SecretDbContext context;

        public ScoreBoardService(SecretDbContext _context)
        {
            context = _context;
        }
        public async Task<IEnumerable<ScoreViewModel>> GetScores()
        {
            var scores =await context.AppUserGames.ToListAsync();
            return new List<ScoreViewModel>();
        }
    }
}
