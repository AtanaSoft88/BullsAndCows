using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SecretNumberGame.Services.Contracts;
using SecretNumberGame.Services.Models;
using System.Collections.Immutable;
using System.Security.Claims;

namespace SecretNumberGame.Controllers
{
    public class GameController : Controller
    {
        private readonly ISecretNumberService service;
        private readonly ISaveResultService saveResultService;

        public GameController(ISecretNumberService _service, ISaveResultService _saveResultService)
        {
            this.service = _service;
            saveResultService = _saveResultService;
        }
        [HttpGet]
        public async Task<IActionResult> GetSecretNumber()
        {
            SecretNumberViewModel modelNumber = await service.GetSecretNumberAsync();

            return View(modelNumber);
        }
        [HttpPost]
        public async Task<IActionResult> FindSecretNumber(string nums, int num, int col)
        {
            var modelNumber = await service.GetModelAsJson(nums,num,col);
            return Json(new { data = modelNumber });
        }

        [HttpPost]
        public async Task<IActionResult> CreateARecord(string secretNum, DateTime startDt, string bombs)
        {
            var userId = User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var model =  await saveResultService.CollectModelProperties(secretNum, startDt, bombs, userId);
            if (model == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }

    }
}
