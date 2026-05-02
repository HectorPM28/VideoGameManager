using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VideoGameManager.Models;
using VideoGameManager.Services;

namespace VideoGameManager.Pages.Games
{
    public class IndexModel : PageModel
    {
        private readonly GameService _service;
        public IndexModel(GameService service)
        {
            _service = service;
        }
        public List<Game> Games { get; set; }
        public void OnGet()
        {
            Games = _service.GetAll();
        }
        public IActionResult OnPostDelete(int id)
        {
            _service.Delete(id);
            return RedirectToPage();
        }
    }
}
