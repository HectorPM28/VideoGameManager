using System.Text.Json;
using VideoGameManager.Data;
using VideoGameManager.Models;

namespace VideoGameManager.Services
{
    public class GameService
    {
        private readonly string _path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "data", "Games.json");

        public void SaveGames(List<Game> games)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };

            string jsonString = JsonSerializer.Serialize(games, options);
            File.WriteAllText(_path, jsonString);
        }
        public List<Game> GetAll()
        {
            if (!File.Exists(_path))
            {
                var initialGames = new List<Game>
            {
                new (){Id = 1, Title = "Danganronpa", Genre = "Visual Novel", Year = 2010, Score = 9, Description = "16 students in a killing game"},
                new (){Id = 2, Title = "Danganronpa 2", Genre = "Visual Novel", Year = 2012, Score = 9, Description = "16 students in a killing game"},
                new (){Id = 3, Title = "Danganronpa v3", Genre = "Visual Novel", Year = 2017, Score = 9, Description = "16 students in a killing game"}
            };
                SaveGames(initialGames);
                return initialGames;
            }

            string jsonString = File.ReadAllText(_path);
            if (string.IsNullOrWhiteSpace(jsonString)) return new List<Game>();

            return JsonSerializer.Deserialize<List<Game>>(jsonString) ?? new List<Game>();
        }
        public Game? GetById(int id) => GetAll().FirstOrDefault(g => g.Id == id);
        public void Add(Game game)
        {
            var games = GetAll();

            int nextId = games.Any() ? games.Max(x => x.Id) + 1 : 1;
            game.Id = nextId;

            games.Add(game);
            GameLog.InsertInfoInLog("Addded", game.Title);
            SaveGames(games);
        }
        public void Update(Game game)
        {
            var games = GetAll();
            var index = games.FindIndex(g => g.Id == game.Id);
            if (index >= 0)
            {
                games[index] = game;
                GameLog.InsertInfoInLog("Updated", game.Title);
                SaveGames(games);
            }
        }
        public void Delete(int id)
        {
            var games = GetAll();
            var game = games.FirstOrDefault(g => g.Id == id);
            games.RemoveAll(g => g.Id == id);
            GameLog.InsertInfoInLog("deleted", game.Title);
            SaveGames(games);
        }
    }
}
