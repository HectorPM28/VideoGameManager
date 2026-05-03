using System.Text.Json;
using VideoGameManager.Data;
using VideoGameManager.Models;

namespace VideoGameManager.Services
{
    public class GameService
    {
        private List<Game> _games = new();
        public GameService(GameRepository repository)
        {
            _games = repository.LoadGames();
        }
        public List<Game> GetAll() => _games;
        public Game? GetById(int id) => GetAll().FirstOrDefault(g => g.Id == id);
        public void Add(Game game)
        {
            int nextId = _games.Any() ? _games.Max(x => x.Id) + 1 : 1;
            game.Id = nextId;

            _games.Add(game);
            GameLog.InsertInfoInLog("Addded", game.Title);
        }
        public void Update(Game game)
        {
            var index = _games.FindIndex(g => g.Id == game.Id);
            if (index >= 0)
            {
                _games[index] = game;
                GameLog.InsertInfoInLog("Updated", game.Title);
            }
        }
        public void Delete(int id)
        {
            var game = _games.FirstOrDefault(g => g.Id == id);
            _games.RemoveAll(g => g.Id == id);
            GameLog.InsertInfoInLog("deleted", game.Title);
        }
    }
}
