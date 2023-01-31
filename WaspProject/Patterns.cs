using System.Reflection;

namespace WaspProject
{
    public class Patterns
    {
        private static readonly string s_directoryPath = Assembly.GetExecutingAssembly().Location.Replace("WaspProject.dll","AppData");
        //private static readonly string s_directoryPath = "C:\\Users\\user\\source\\repos\\WaspProject\\WaspProject\\AppData";

        public static string GetJsonIdPattern(int id)
        {
            return $"\"Id\":{id}";
        }

        public static string GetCinemasFilePath()
        {
            return Path.Combine(s_directoryPath, "Cinemas.json");
        }
        public static string GetFilmsFilePath(int id)
        {
            return Path.Combine(s_directoryPath, $"Films\\Cinema{id}.json");
        }
        public static string GetIdsFilePath()
        {
            return Path.Combine(s_directoryPath, "Ids.json");
        }

        public static string GetAvatarsFilePath(int id, int cinemaId)
        {
            return $"\\Resources\\Images\\Avatars\\avatar{cinemaId}_{id}";
        }
    }
}
