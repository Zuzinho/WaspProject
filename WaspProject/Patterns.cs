namespace WaspProject
{
    public class Patterns
    {
        private static readonly string s_directoryPath = "C:\\Users\\user\\source\\repos\\WaspProject\\WaspProject\\AppData\\";


        public static string GetJsonIdPattern(int id)
        {
            return $"\"Id\":{id}";
        }

        public static string GetCinemasFilePath()
        {
            return s_directoryPath + "Cinemas.json";
        }
        public static string GetFilmsFilePath(int id)
        {
            return s_directoryPath + "Films\\" + "Cinema" + id + ".json";
        }
        public static string GetAvatarsFilePath(string fileName)
        {
            return "\\Resources\\Images\\Avatars\\" + fileName;
        }
    }
}
