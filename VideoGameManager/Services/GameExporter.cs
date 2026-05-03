using System.Text;
using VideoGameManager.Models;

namespace VideoGameManager.Services
{
    public class GameExporter
    {
        public byte[] ExportToCsv(List<Game> games)
        {
            var builder = new StringBuilder();

            builder.AppendLine("Id,Title,Genre,Year,Score");

            foreach (var game in games)
            {
                var row = string.Join(",", new[] {
                    game.Id.ToString(),
                    game.Title.Replace(",", ""),
                    game.Genre,
                    game.Year.ToString(),
                    game.Score.ToString()
                });
                builder.AppendLine(row);
            }
            return Encoding.UTF8.GetBytes(builder.ToString());
        }

        public List<Game> ImportFromCsv(byte[] csvBytes)
        {
            var games = new List<Game>();
            var content = Encoding.UTF8.GetString(csvBytes);

            var lines = content.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 1; i < lines.Length; i++)
            {
                var parts = lines[i].Split(',');
                if (parts.Length == 5)
                {
                    games.Add(new Game
                    {
                        Id = int.Parse(parts[0]),
                        Title = parts[1],
                        Genre = parts[2],
                        Year = int.Parse(parts[3]),
                        Score = double.Parse(parts[4], System.Globalization.CultureInfo.InvariantCulture)
                    });
                }
            }
            return games;
        }
    }
}
