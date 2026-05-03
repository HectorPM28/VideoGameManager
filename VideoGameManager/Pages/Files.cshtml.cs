using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VideoGameManager.Models;
using VideoGameManager.Services;

namespace VideoGameManager.Pages
{
    public class FilesModel : PageModel
    {
        private readonly GameService _gameService;
        private readonly GameExporter _exporter;
        private readonly GameRanking _xmlExporter;
        private readonly GameRepository _gameRepository;
        private List<Game> _games;

        public FilesModel(GameService gameService, GameExporter exporter, GameRanking xmlExporter, GameRepository gameRepository)
        {
            _gameService = gameService;
            _exporter = exporter;
            _xmlExporter = xmlExporter;
            _gameRepository = gameRepository;
        }
        public IActionResult OnPostExportJson()
        {
            var games = _gameService.GetAll();

            _gameRepository.SaveGames(games);

            return RedirectToPage();
        }
        public IActionResult OnPostImportJson()
        {
            _games = _gameRepository.LoadGames();
            _gameService.GetAll().Clear();
            foreach (var game in _games)
            {
                _gameService.GetAll().Add(game);
            }
            return RedirectToPage("/Games/Index");
        }
        public IActionResult OnPostDownloadRanking()
        {
            var games = _gameService.GetAll();
            byte[] xmlBytes = _xmlExporter.ExportToXmlRanking(games);

            return File(xmlBytes, "application/xml", "ranking.xml");
        }

        public IActionResult OnPostExport()
        {
            var games = _gameService.GetAll();

            byte[] csvBytes = _exporter.ExportToCsv(games);

            return File(csvBytes, "text/csv", "games.csv");
        }
        public IActionResult OnPostImport(IFormFile archiveCsv)
        {
            if (archiveCsv == null || archiveCsv.Length == 0)
            {
                return Page();
            }

            using (var ms = new MemoryStream())
            {
                archiveCsv.CopyTo(ms);
                byte[] fileBytes = ms.ToArray();

                List<Game> importedGames = _exporter.ImportFromCsv(fileBytes);

                var allGames = _gameService.GetAll();

                foreach (var game in importedGames)
                {
                    int nextId = allGames.Any() ? allGames.Max(x => x.Id) + 1 : 1;
                    game.Id = nextId;
                    allGames.Add(game);
                }

            }

            return RedirectToPage("/Games/Index");
        }
    }
}