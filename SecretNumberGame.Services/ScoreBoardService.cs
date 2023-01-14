using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SecretNumberGame.Data;
using SecretNumberGame.Data.DataModels;
using SecretNumberGame.Services.Contracts;
using SecretNumberGame.Services.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretNumberGame.Services
{
    public class ScoreBoardService : IScoreBoardService
    {
        private readonly SecretDbContext context;
        private readonly UserManager<AppUser> userManager;

        public ScoreBoardService(SecretDbContext _context, UserManager<AppUser> userManager)
        {
            context = _context;
            this.userManager = userManager;
        }

        public async Task ClearScore()
        {
            var appUserGames = await context.AppUserGames.ToListAsync();
            var games = await context.Games.ToListAsync();
            context.AppUserGames.RemoveRange(appUserGames);
            context.Games.RemoveRange(games);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MyScoreViewModel>> GetMyScore(string userId)
        {
            var userGames = await context.AppUserGames.Include(x => x.Game).Where(u => u.UserId == Guid.Parse(userId)).Select(x => new MyScoreViewModel
            {
                UserNames = $"{x.AppUser.FirstName} {x.AppUser.LastName}",
                Email = x.AppUser.Email,
                StartTime = x.Game.GameStartTime.ToString("HH:mm:ss"),
                EndTime = x.Game.GameFinishTime.ToString("HH:mm:ss"),
                GameDuration = x.Game.Duration.TotalSeconds.ToString("f2"),
                GoldForTheGame = x.Game.GoldEarned == null ? 0 : x.Game.GoldEarned,
                SecretNumber = x.Game.SecretNumber,

            }).ToListAsync();
            return userGames;
        }

        public async Task<IEnumerable<ScoreViewModel>> GetScore()
        {
            var users = await context.Users.Include(ug => ug.UserGames).ThenInclude(g => g.Game).Select(x => new ScoreViewModel
            {
                UserNames = $"{x.FirstName} {x.LastName}",
                UserTotalGames = x.UserGames.Count(),
                TotalGold = x.UserGames.Count() > 0 ? x.UserGames.Sum(g => g.Game.GoldEarned).Value : 0,
                AverageGoldEarned = x.UserGames.Count() > 0 ? x.UserGames.Average(g => g.Game.GoldEarned).Value : 0,
                BestDuration = x.UserGames.Count() > 0 ? x.UserGames
                                                     .OrderBy(x => x.Game.Duration)
                                                     .Select(x => x.Game.Duration.TotalSeconds)
                                                     .First()
                                                     .ToString("f2") : 0.ToString(),
                GoldAllGames = x.UserGames.Count() > 0 ? x.UserGames
                                                     .Select(x => x.Game.GoldEarned)
                                                     .ToList() : new List<int?>()
            }).ToListAsync();
            var count = 0;
            foreach (var user in users.OrderByDescending(y => y.TotalGold).ThenBy(x => double.Parse(x.BestDuration)))
            {
                if (count != 0)
                {
                    user.HasBestTime = false;
                }
                else
                {
                    if (double.Parse(user.BestDuration) > 0)
                    {
                        user.HasBestTime = true;
                        user.HasBestTotalScore = true;
                    }
                }
                count++;
            }
            return users;
        }
    }
}
