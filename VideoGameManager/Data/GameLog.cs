namespace VideoGameManager.Data
{
    public class GameLog
    {
        static string proyectoRuta = Directory.GetCurrentDirectory();

        public static string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "data", "GameLog.txt");


        public static void InsertInfoInLog(string action, string gameTitle)
        {
            try
            {
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine("==================================================");
                    sw.WriteLine($"Game log - Time {DateTime.Now}");
                    sw.WriteLine("==================================================");
                    sw.WriteLine($"{gameTitle} got {action}");
                    sw.WriteLine("--------------------------------------------------");
                    sw.WriteLine();
                }

            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"No s'ha trobat el fitxer");
            }
        }
    }
}
