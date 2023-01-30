using System.Text.Json;
using WaspProject.Model;

namespace WaspProject.DataBase
{
    public class DataBaseReader: DataBase
    {
        public static List<Cinema> GetCinemas()
        {
            return GetObjects<Cinema>(s_cinemasFilePath);
        }


        public static List<Film> GetFilms()
        {
            List<Film> films = new();
            Parallel.ForEach(GetCinemas(),
                cinema =>
                films.AddRange(GetFilms(cinema.Id))
                );
            return films;
        }
        public static List<Film> GetFilms(int cinemaId)
        {
            return GetObjects<Film>(Patterns.GetFilmsFilePath(cinemaId));
        }


        private static List<T> GetObjects<T>(string filePath)
        {
            List<T> objects = new();
            try
            {
                string jsonString = File.ReadAllText(filePath);
                Parallel.ForEach(StringParser.Split(jsonString, s_separator),
                    jsonStringObject =>
                    objects.Add(JsonSerializer.Deserialize<T>(jsonStringObject))
                    );
                return objects;
            }
            catch
            {
                return null;
            }
        }

    }
}
