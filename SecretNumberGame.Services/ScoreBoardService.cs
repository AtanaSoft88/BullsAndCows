using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SecretNumberGame.Data;
using SecretNumberGame.Data.DataModels;
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

        public ScoreBoardService(SecretDbContext _context, UserManager<AppUser> userManager)
        {
            context = _context;            
        }

        public async Task ClearScore()
        {
            var appUserGames =await context.AppUserGames.ToListAsync();
            var games = await context.Games.ToListAsync();
            context.AppUserGames.RemoveRange(appUserGames);
            context.Games.RemoveRange(games);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ScoreViewModel>> GetScore()
        {            
            var users = await context.Users.Include(ug=>ug.UserGames).ThenInclude(g=>g.Game).Select(x=> new ScoreViewModel 
            {
                UserNames = $"{x.FirstName} {x.LastName}",
                UserTotalGames = x.UserGames.Count(),
                TotalGold = x.UserGames.Count() > 0 ? x.UserGames.Sum(g => g.Game.GoldEarned).Value : 0,
                AverageGoldEarned = x.UserGames.Count() > 0 ? x.UserGames.Average(g => g.Game.GoldEarned).Value :0,
                BestDuration = x.UserGames.Count()>0? x.UserGames
                                                     .OrderBy(x => x.Game.Duration)
                                                     .Select(x => x.Game.Duration.TotalSeconds)
                                                     .First()
                                                     .ToString("f2"):0.ToString(),
                GoldAllGames = x.UserGames.Count()>0? x.UserGames
                                                     .Select(x => x.Game.GoldEarned)
                                                     .ToList():new List<int?>()
            }).ToListAsync();
            var maxGold = -1;
            if (users.Select(x=>x.UserTotalGames).Any())
            {
                maxGold = 1;
            }
            
            foreach (var user in users)
            {
                if (user.TotalGold >= maxGold)
                {
                    maxGold = user.TotalGold;
                    user.HasBestTotalScore = true;
                }
            }
            double bestTime = 10000;
            double lastBestTime = 0;
            foreach (var user in users)
            {
                double time = double.Parse(user.BestDuration);
                if (time < bestTime && time != 0)
                {
                    bestTime = time;
                    lastBestTime = time;
                    user.HasBestTime = true;
                }
            }
            foreach (var user in users)
            {
                if (double.Parse(user.BestDuration) != lastBestTime)
                {
                    user.HasBestTime = false;
                }
            }            
           
            return users;
        }
    }
}
