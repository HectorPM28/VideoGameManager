using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VideoGameManager.Models;
using VideoGameManager.Services;

namespace VideoGameManager.Pages.Games
{
    public class UpdateGameModel : PageModel
    {
        private readonly GameService _service;

        public UpdateGameModel(GameService service)
        {
            _service = service;
        }
        [BindProperty]
        public Game NewGame { get; set; }

        public void OnGet(int id)
        {
            NewGame = _service.GetById(id);
        }
        public IActionResult OnPostUpdate()
        {

            _service.Update(NewGame);

            return RedirectToPage("/Games/Index");
        }
    }
}
