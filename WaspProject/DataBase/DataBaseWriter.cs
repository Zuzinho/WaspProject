using System.Collections.ObjectModel;
using System.Text.Json;
using WaspProject.Model;

namespace WaspProject.DataBase
{
    public class DataBaseWriter: DataBase
    {
        // Первый элемент - last id для фильмов
        private static readonly List<int> s_objectsLastId = JsonSerializer.Deserialize<List<int>>(File.ReadAllText(Patterns.GetIdsFilePath())); 


        public static int AddCinema(Cinema cinema, bool needCheck = true)
        {
            if (AddObject(s_cinemasFilePath, cinema, needCheck)) CreateCinemaFile(cinema.Id);
            return cinema.Id;
        }
        public static int AddCreateCinema(string name, string address)
        {
            Cinema cinema = new(++s_objectsLastId[0], name, address);
            s_objectsLastId.Add(0);
            ReWriteIdsFile();
            return AddCinema(cinema, false);
        }
        public static void AddCinemas(List<Cinema> cinemas)
        {
            AddObjects(cinemas, AddCinema);
        }

        public static void DeleteCinema(int cinemaId)
        {
            ReAddObjects(s_cinemasFilePath, cinemaId);
            File.Delete(Patterns.GetFilmsFilePath(cinemaId));
        }


        public static int AddFilm(Film film, bool needCheck = true)
        {
            string filePath = Patterns.GetFilmsFilePath(film.CinemaId);
            AddObject(filePath, film, needCheck);
            return film.Id;
        }
        public static int AddCreateFilm(string name, string genre, string description, int cinemaId, ObservableCollection<Session> sessions)
        {
            Film film = new(++s_objectsLastId[cinemaId],name,genre,description,cinemaId,sessions);
            ReWriteIdsFile();
            return AddFilm(film, false);
        }
        public static void AddFilms(List<Film> films)
        {
            AddObjects(films, AddFilm);
        }

        public static void DeleteFilm(int filmId, int cinemaId)
        {
            string filePath = Patterns.GetFilmsFilePath(cinemaId);
            ReAddObjects(filePath, filmId);
        }


        private static bool AddObject(string filePath, AbstractCinemaObject cinemaObject, bool needCheck)
        {
            if (needCheck && DataBaseChecker.ContainsSameId(filePath, cinemaObject.Id)) return false;
            string jsonString = JsonSerializer.Serialize(cinemaObject);
            using var streamWriter = new StreamWriter(new FileStream(filePath, FileMode.Append));
            streamWriter.Write(jsonString + s_separator);
            return true;
        }
        private static void AddObjects<T>(List<T> cinemaObjects, Func<T, bool, int> func)
        {
            Parallel.ForEach(cinemaObjects,
                cinemaObject =>
                func(cinemaObject, true)
                );
        }

        private static void ReWriteIdsFile()
        {
            using var streamWriter = new StreamWriter(new FileStream(Patterns.GetIdsFilePath(),FileMode.Open));
            string jsonString = JsonSerializer.Serialize(s_objectsLastId);
            streamWriter.Write(jsonString);
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
    }
}
