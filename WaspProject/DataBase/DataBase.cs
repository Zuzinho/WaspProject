namespace WaspProject.DataBase
{
    public abstract class DataBase
    {
        protected static readonly string s_cinemasFilePath = Patterns.GetCinemasFilePath();
        protected static readonly char s_separator = '\n';


        protected static void CleanFile(string fileName)
        {
            File.Open(fileName, FileMode.Create).Close();
        }

        protected static void CreateCinemaFile(int cinemaId)
        {
            string fileName = Patterns.GetFilmsFilePath(cinemaId);
            if (!File.Exists(fileName)) File.Create(fileName).Close();
        }
    }
}
