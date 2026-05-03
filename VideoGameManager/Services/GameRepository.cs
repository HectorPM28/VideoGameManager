using System.Text.Json;
using VideoGameManager.Models;

namespace VideoGameManager.Services
{
    public class GameRepository
    {
        private readonly string _path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "data", "Games.json");
        private string _json;

        public List<Game> LoadGames()
        {
            if (!File.Exists(_path))
            {
                return new List<Game>();
            }
            _json = File.ReadAllText(_path);

            try
            {
                return JsonSerializer.Deserialize<List<Game>>(_json) ?? new List<Game>();
            }
            catch (Exception e)
            {
                return new List<Game>();
            }
        }


        public void SaveGames(List<Game> games)
        {
            _json = JsonSerializer.Serialize(games, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_path, _json);
        }
    }
}
