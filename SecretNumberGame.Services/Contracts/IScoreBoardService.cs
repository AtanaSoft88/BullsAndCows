using SecretNumberGame.Services.Models;

namespace SecretNumberGame.Services.Contracts
{
    public interface IScoreBoardService
    {
        Task<IEnumerable<ScoreViewModel>> GetScores();
    }
}
