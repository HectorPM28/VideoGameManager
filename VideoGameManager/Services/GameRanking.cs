using System.Xml.Linq;
using VideoGameManager.Models;

namespace VideoGameManager.Services
{
    public class GameRanking
    {
        public byte[] ExportToXmlRanking(List<Game> games)
        {
            var ranking = games.OrderByDescending(g => g.Score).ToList();

            var doc = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("AppConfig",
                    new XElement("AppTitle", "VideoGame Ranking"),
                    new XElement("Games",
                        ranking.Select(g => new XElement("Game",
                            new XElement("id", g.Id),
                            new XElement("score", g.Score.ToString()),
                            new XElement("title", g.Title),
                            new XElement("genre", g.Genre),
                            new XElement("year", g.Year),
                            new XElement("description", g.Description)
                        ))
                    )
                )
            );

            using (var ms = new MemoryStream())
            {
                doc.Save(ms);
                return ms.ToArray();
            }
        }
    }
}
