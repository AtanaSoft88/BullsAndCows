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
    public class SaveResultService : ISaveResultService
    {
        private readonly SecretDbContext context;

        public SaveResultService(SecretDbContext _context)
        {
            this.context = _context;
        }

        public async Task<ResultScoreViewModel> CollectModelProperties(string secretNum, DateTime startDt, string bombs, string userId)
        {
            int penaltyPoints = 0;
            var totalNegativePoints = int.Parse(bombs);
            if (totalNegativePoints > 0 && totalNegativePoints <= 3)
            {
                penaltyPoints = totalNegativePoints * (-3);
            }
            else if (totalNegativePoints > 3)
            {
                penaltyPoints = totalNegativePoints * (-4);
            }
            ResultScoreViewModel model = new ResultScoreViewModel()
            {
                UserId = Guid.Parse(userId),
                SecretNumber = secretNum,
                StartGameTime = startDt,
                EndGameTime = DateTime.Now,
                PenaltyPoints = penaltyPoints,
                
            };
            var succeededModel = await AddResultToDb(model);
            return succeededModel;
        }
        public async Task<ResultScoreViewModel> AddResultToDb(ResultScoreViewModel model)
        {
            var game = new Game()
            {
                GameStartTime = model.StartGameTime,
                GameFinishTime = model.EndGameTime,
                Duration = model.Duration,
                GoldEarned = model.PenaltyPoints,
                SecretNumber = model.SecretNumber,
                
            };
            if (game.Duration.TotalSeconds <= 60) // up to 1 min
            {
                game.GoldEarned += 100;
            }
            else if (game.Duration.TotalSeconds > 60 && game.Duration.TotalSeconds <= 120) // 2 min
            {
                game.GoldEarned += 70;
            }
            else if (game.Duration.TotalSeconds > 120 && game.Duration.TotalSeconds <= 180) // 3min
            {
                game.GoldEarned += 40;
            }
            else if (game.Duration.TotalSeconds > 180 && game.Duration.TotalSeconds <= 240) // 4min
            {
                game.GoldEarned += 30;
            }
            else
            {
                game.GoldEarned += 20;
            }

            if (game.GoldEarned < 0)
            {
                game.GoldEarned = 0;
            }
            await context.AppUserGames.AddAsync(new AppUserGame() { Game = game, UserId = model.UserId });
            await context.SaveChangesAsync();
            return model;
        }
    }
}
