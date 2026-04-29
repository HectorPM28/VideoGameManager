using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VideoGameManager.Models;
using VideoGameManager.Services;

namespace VideoGameManager.Pages.Games
{
    public class GameDetailModel : PageModel
    {
        private readonly GameService _service;

        public GameDetailModel(GameService service)
        {
            _service = service;
        }
        public Game Game { get; set; }
        public void OnGet(int id)
        {
            Game = _service.GetById(id);
        }
    }
}
