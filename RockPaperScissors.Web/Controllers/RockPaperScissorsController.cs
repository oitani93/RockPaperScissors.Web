using Microsoft.AspNetCore.Mvc;
using RockPaperScissors.Web.Models;
using RockPaperScissors.Services.Interface;
using static RockPaperScissors.Web.Infrustructure.ApplicationConstants;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace RockPaperScissors.Web.Controllers
{
    [Area("RockPaperScissors")]
    [Route("RockPaperScissors")]
    public class RockPaperScissorsController : Controller
    {
        private readonly IRockPaperScissorsService _rockPaperScissorsService;
        private const string sessionKey = "GameSessionKey";

        public RockPaperScissorsController(IRockPaperScissorsService rockPaperScissorsService)
        {
            _rockPaperScissorsService = rockPaperScissorsService;
        }

        [ActionName("RockPaperScissorsGame")]
        [HttpGet("RockPaperScissorsGame")]
        public IActionResult RockPaperScissorsGame()
        {
            var gameViewModel = new PlayersHomePageViewModel();
            SetUpComputerPlayer(gameViewModel);
            SetOptions(gameViewModel);//
            return View("/Views/RockPaperScissorsGame/RockPaperScissorsGame.cshtml", gameViewModel);
        }

        [ActionName("RockPaperScissorsGame")]
        [HttpPost("RockPaperScissorsGame")]
        [ValidateModelState]
        [ValidateAntiForgeryToken]
        public IActionResult RockPaperScissorsGame(PlayersHomePageViewModel playersHomePageViewModel)
        {
            playersHomePageViewModel.PlayerTwo.ScoreSheet = new PlayerScoreSheetViewModel();
            playersHomePageViewModel.PlayerOne.ScoreSheet = new PlayerScoreSheetViewModel();
            var gameViewModel = new RockPaperScissorsGameViewModel(playersHomePageViewModel.PlayerOne,
                playersHomePageViewModel.PlayerTwo);
           
            HttpContext.Session.SetString(sessionKey, JsonConvert.SerializeObject(gameViewModel));
            return PartialView("/Views/RockPaperScissorsGame/_RockPaperScissorsGamePartial.cshtml", gameViewModel);
        }

        [ActionName("SubmitSelection")]
        [HttpPost("SubmitSelection")]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitSelection(int action)
        {
            var sessionData = HttpContext.Session.GetString(sessionKey);
            var viewModel = JsonConvert.DeserializeObject<RockPaperScissorsGameViewModel>(sessionData);
            viewModel.SubmitSelection((Actions)action);
            HttpContext.Session.SetString(sessionKey, JsonConvert.SerializeObject(viewModel));
            return PartialView("/Views/RockPaperScissorsGame/_RockPaperScissorsGamePartial.cshtml", viewModel);
        }

        [ActionName("RockPaperScissorsGamePartial")]
        [HttpGet("RockPaperScissorsGamePartial")]
        public IActionResult RockPaperScissorsGamePartial()
        {
            var sessionData = HttpContext.Session.GetString(sessionKey);
            var viewModel = JsonConvert.DeserializeObject<RockPaperScissorsGameViewModel>(sessionData);
            viewModel.SetRowsClass();
            return View("/Views/RockPaperScissorsGame/_RockPaperScissorsGamePartial.cshtml", viewModel);
        }

        [ActionName("SaveUserScoreSheet")]
        [HttpPost("SaveUserScoreSheet")]
        [ValidateModelState]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveUserScoreSheet(UserInputViewModel playerOne)
        {
            await _rockPaperScissorsService.SaveUserScoreSheetAsync(playerOne.UserName, playerOne.EmailAddress, playerOne.MobileNumber);
            return Json(true);
        }

        private void SetUpComputerPlayer(PlayersHomePageViewModel gameViewModel)
        {
            gameViewModel.PlayerTwo = new PlayerViewModel { UserName = "Computer", PlayerType = PlayerType.COMPUTER };
        }

        private void SetOptions(PlayersHomePageViewModel gameViewModel)
        {
            gameViewModel.Players = new List<SelectListItem>
            {
                new SelectListItem { Text = "Computer", Value = JsonConvert.SerializeObject(gameViewModel.PlayerTwo) }
            };
        }
    }
}
