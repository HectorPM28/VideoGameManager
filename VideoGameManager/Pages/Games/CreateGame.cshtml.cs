using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VideoGameManager.Models;
using VideoGameManager.Services;

namespace VideoGameManager.Pages.Games
{
    public class CreateGameModel : PageModel
    {
        private readonly GameService _service;

        public CreateGameModel(GameService gameService)
        {
            _service = gameService;
        }
        [BindProperty]
        public string GameName { get; set; }
        [BindProperty]
        public string GameGenre { get; set; }
        [BindProperty]
        public int GameScore { get; set; }
        [BindProperty]
        public int GameYear { get; set; }
        [BindProperty]
        public string GameDescription { get; set; }

        public IActionResult OnPostAdd()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var newGame = new Game
            {
                Title = GameName,
                Genre = GameGenre,
                Score = GameScore,
                Year = GameYear,
                Description = GameDescription
            };

            _service.Add(newGame);

            return RedirectToPage("/Games/Index");
        }
    }
}
