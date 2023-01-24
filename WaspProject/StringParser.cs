namespace WaspProject
{
    public class StringParser
    {
        public static List<string> Split(string text, char separator = '\n')
        {
            return text.Split(separator).Where(jsonString => !jsonString.Equals("")).ToList();
        }
    }
}
