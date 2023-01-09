using Microsoft.AspNetCore.Mvc;
using SecretNumberGame.Data;
using SecretNumberGame.Services.Contracts;
using SecretNumberGame.Services.Models;

namespace SecretNumberGame.Controllers
{
    public class ScoreBoardController : BaseController
    {
        
        private readonly IScoreBoardService service;

        public ScoreBoardController(IScoreBoardService _service)
        {            
            service = _service;
        }

        [HttpGet]
        public async Task<IActionResult> GeAllScoresAsync()
        {
            var modelNumber = await service.GetScores();

            return View(modelNumber);
        }
    }
}
