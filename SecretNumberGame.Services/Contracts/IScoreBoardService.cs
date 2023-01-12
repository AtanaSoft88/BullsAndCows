using SecretNumberGame.Services.Models;

namespace SecretNumberGame.Services.Contracts
{
    public interface IScoreBoardService
    {
        Task<IEnumerable<ScoreViewModel>> GetScore();
        Task<IEnumerable<MyScoreViewModel>> GetMyScore(string userId);
        Task ClearScore();
    }
}
