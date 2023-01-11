using Microsoft.AspNetCore.Authorization;
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
        [AllowAnonymous]
        public async Task<IActionResult> GetAllUsersScore()
        {
            var modelNumber = await service.GetScore();

            return View(modelNumber);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetScoreBoard()
        {
            await service.ClearScore();

            return RedirectToAction(nameof(GetAllUsersScore));
        }
    }
}
