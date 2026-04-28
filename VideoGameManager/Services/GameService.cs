using VideoGameManager.Models;

namespace VideoGameManager.Services
{
    public class GameService
    {
        private readonly List<Game> _games = new()
        {
            new (){Id = 1, Title = "Danganronpa", Genre = "Visual Novel", Year = 2010, Score = 9, Description = "16 students in a killing game"},
            new (){Id = 2, Title = "Danganronpa 2", Genre = "Visual Novel", Year = 2012, Score = 9, Description = "16 students in a killing game"},
            new (){Id = 3, Title = "Danganronpa v3", Genre = "Visual Novel", Year = 2017, Score = 9, Description = "16 students in a killing game"}
        };
        private int _nextId = 4;

        public List<Game> GetAll() => _games;
        public Game? GetById(int id) => _games.FirstOrDefault(g => g.Id == id);
        public void Add(Game game)
        {
            game.Id = _nextId++;
            _games.Add(game);
        }
        public void Update(Game game)
        {
            var index = _games.FindIndex(g => g.Id == game.Id);
            if (index >= 0) _games[index] = game;
        }
        public void Delete(int id) => _games.RemoveAll(g => g.Id == id);
    }
}
