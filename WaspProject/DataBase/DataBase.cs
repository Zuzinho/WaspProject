using System.Text.Json;
using WaspProject.Model;

namespace WaspProject.DataBase
{
    public class DataBase
    {
        private static readonly string s_cinemasFilePath = Patterns.GetCinemasFilePath();

        private static readonly char s_separator = '\n';


        public static List<Cinema> GetCinemas()
        {
            return GetObjects<Cinema>(s_cinemasFilePath);
        }

        public static void AddCinema(Cinema cinema)
        {
            if (AddObject(s_cinemasFilePath, cinema)) CreateCinemaFile(cinema.Id);
        }
        public static void AddCinemas(List<Cinema> cinemas)
        {
            Parallel.ForEach(cinemas,
                cinema =>
                AddCinema(cinema)
                );
        }

        public static void DeleteCinema(int cinemaId)
        {
            ReAddObjects(s_cinemasFilePath, cinemaId);
            File.Delete(Patterns.GetFilmsFilePath(cinemaId));
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

        public static void AddFilm(Film film)
        {
            string filePath = Patterns.GetFilmsFilePath(film.CinemaId);
            AddObject(filePath, film);
        }
        public static void AddFilms(List<Film> films)
        {
            Parallel.ForEach(films,
                film =>
                AddFilm(film)
                );
        }

        public static void DeleteFilm(int filmId, int cinemaId)
        {
            string filePath = Patterns.GetFilmsFilePath(cinemaId);
            ReAddObjects(filePath, filmId);
        }


        public static void CleanFile(string fileName)
        {
            File.Open(fileName, FileMode.Create).Close();
        }


        private static bool AddObject(string filePath, AbstractCinemaObject cinemaObject)
        {
            if (DataBaseChecker.ContainsSameId(filePath, cinemaObject.Id)) return false;
            string jsonString = JsonSerializer.Serialize(cinemaObject);
            using var streamWriter = new StreamWriter(new FileStream(filePath, FileMode.Append));
            streamWriter.Write(jsonString + s_separator);
            return true;
        }

        private static void ReAddObjects(string filePath, int id)
        {
            List<string> jsonStrings = StringParser.Split(File.ReadAllText(filePath), s_separator);
            string idPattern = Patterns.GetJsonIdPattern(id);
            jsonStrings = jsonStrings.Where(jsonString => !jsonString.Contains(idPattern)).ToList();
            CleanFile(filePath);
            using var streamWriter = new StreamWriter(new FileStream(filePath, FileMode.Append));
            foreach (string jsonString in jsonStrings) streamWriter.Write(jsonString + s_separator);
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


        private static void CreateCinemaFile(int cinemaId)
        {
            string fileName = Patterns.GetFilmsFilePath(cinemaId);
            if (!File.Exists(fileName)) File.Create(fileName).Close();
        }
    }
}
